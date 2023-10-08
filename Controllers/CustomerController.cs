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
    [Route("api/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        public CustomerController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Customer> customers =
                    _ctx.Customers
                    .Include(x => x.Address)
                    .ToList();

                if (customers.Count == 0)
                {
                    return NotFound("Nenhum cliente encontrado.");
                }

                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar clientes: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                Customer customer =
                    _ctx.Customers
                    .Include(x => x.Address)
                    .FirstOrDefault(x => x.Name == name);

                if (customer != null)
                {
                    return Ok(customer);
                }

                return NotFound($"Cliente com o nome '{name}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar cliente: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                Address address = _ctx.Adresses.Find(customer.AddressId);

                if (address == null)
                {
                    return NotFound("Endereço não encontrado.");
                }

                customer.CreatedAt = DateTime.UtcNow;
                customer.Address = address;

                _ctx.Customers.Add(customer);
                _ctx.SaveChanges();

                return Created("", customer);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar cliente: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Customer customer)
        {
            try
            {
                Address address = _ctx.Adresses.Find(customer.AddressId);

                if (address == null)
                {
                    return NotFound("Endereço não encontrado.");
                }

                Customer existingCustomer = _ctx.Customers.FirstOrDefault(x => x.CustomerId == id);

                if (existingCustomer != null)
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.CPF = customer.CPF;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Email = customer.Email;

                    existingCustomer.Address = address;

                    _ctx.Customers.Update(existingCustomer);
                    _ctx.SaveChanges();

                    return Ok();
                }

                return NotFound($"Cliente com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar cliente: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Customer customer = _ctx.Customers.Find(id);

                if (customer != null)
                {
                    _ctx.Customers.Remove(customer);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Cliente com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir cliente: {e.Message}");
            }
        }
    }
}
