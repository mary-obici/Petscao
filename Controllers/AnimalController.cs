using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Animal")]
    public class AnimalController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public AnimalController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Animal> animals =
                    _ctx.Animals
                    .Include(x => x.Customer)
                    .ToList();

                if (animals.Count == 0)
                {
                    return NotFound("Nenhum animal encontrado.");
                }

                return Ok(animals);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar animais: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                Animal animal =
                    _ctx.Animals
                    .Include(x => x.Customer)
                    .FirstOrDefault(x => x.Name == name);

                if (animal != null)
                {
                    return Ok(animal);
                }

                return NotFound($"Animal com o nome '{name}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar animal: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Animal animal)
        {
            Customer customer = _ctx.Customers.Find(animal.CustomerId);

            if (customer == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            try
            {
                animal.CreatedAt = DateTime.UtcNow;
                animal.Customer = customer;

                _ctx.Animals.Add(animal);
                _ctx.SaveChanges();

                return Created("", animal);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar animal: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Animal animal)
        {
            Customer customer = _ctx.Customers.Find(animal.CustomerId);

            if (customer == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            try
            {
                Animal existingAnimal = _ctx.Animals.FirstOrDefault(x => x.AnimalId == id);

                if (existingAnimal != null)
                {
                    existingAnimal.Name = animal.Name;
                    existingAnimal.Breed = animal.Breed;
                    existingAnimal.Customer = customer;

                    _ctx.Animals.Update(existingAnimal);
                    _ctx.SaveChanges();

                    return Ok();
                }

                return NotFound($"Animal com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar animal: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Animal animal = _ctx.Animals.Find(id);

                if (animal != null)
                {
                    _ctx.Animals.Remove(animal);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Animal com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir animal: {e.Message}");
            }
        }
    }
}
