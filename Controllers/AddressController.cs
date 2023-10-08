using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Address")]
    public class AddressController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        public AddressController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Address> addresses = _ctx.Adresses.ToList();
                if (addresses.Count == 0)
                {
                    return NotFound("Nenhum endereço encontrado.");
                }
                return Ok(addresses);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar endereços: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByStreet/{street}")]
        public IActionResult GetByStreet([FromRoute] string street)
        {
            try
            {
                Address? address = _ctx.Adresses.FirstOrDefault(x => x.Street == street);
                if (address != null)
                {
                    return Ok(address);
                }
                return NotFound($"Endereço com a rua '{street}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar endereço: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Address address)
        {
            try
            {
                address.CreatedAt = DateTime.UtcNow;
                _ctx.Adresses.Add(address);
                _ctx.SaveChanges();
                return Created("", address);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar endereço: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Address address)
        {
            try
            {
                Address? existingAddress = _ctx.Adresses.FirstOrDefault(x => x.AddressId == id);
                if (existingAddress != null)
                {
                    existingAddress.Street = address.Street;
                    existingAddress.Number = address.Number;
                    existingAddress.City = address.City;
                    existingAddress.Neighborhood = address.Neighborhood;
                    existingAddress.CEP = address.CEP;

                    _ctx.Adresses.Update(existingAddress);
                    _ctx.SaveChanges();

                    return Ok();
                }
                return NotFound($"Endereço com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar endereço: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Address? address = _ctx.Adresses.Find(id);
                if (address != null)
                {
                    _ctx.Adresses.Remove(address);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound($"Endereço com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir endereço: {e.Message}");
            }
        }
    }
}
