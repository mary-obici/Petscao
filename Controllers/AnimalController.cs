using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApi.Controllers;

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
                .Include(x =>  x.Customer)
                .ToList();

            return animals.Count == 0 ? NotFound() : Ok(animals);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("post")]
    public IActionResult Post([FromBody] Animal animal)
    {
        try
        {
            Customer customer = _ctx.Customers.Find(animal.CustomerId);

            if (customer == null) {
                return NotFound();
            }

            animal.Customer = customer;

            _ctx.Animals.Add(animal);
            _ctx.SaveChanges();
            
            return Created("", animal);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getByName/{name}")]
    public IActionResult GetByName([FromRoute] string name)
    {
        try
        {
            Animal? animal = 
                _ctx.Animals
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Name == name);
            if (animal != null)
            {
                return Ok(animal);
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            Animal? animal = _ctx.Animals.Find(id);

            if (animal != null)
            {
                _ctx.Animals.Remove(animal);
                _ctx.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("put/{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] Animal animal)
    {
        try
        {
            Customer customer = _ctx.Customers.Find(animal.CustomerId);

            if (customer == null) {
                return NotFound();
            }

            Animal? animals = _ctx.Animals.FirstOrDefault(x => x.AnimalId == id);
            if (animals != null)
            {
                animals.Name = animal.Name;
                animals.Breed = animal.Breed;
                animals.Customer = customer;

                _ctx.Animals.Update(animals);
                _ctx.SaveChanges();
                
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}