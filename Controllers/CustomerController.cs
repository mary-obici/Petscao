using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApi.Controllers;

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
                .Include(x =>  x.Address)
                .ToList();

            return customers.Count == 0 ? NotFound() : Ok(customers);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("post")]
    public IActionResult Post([FromBody] Customer customer)
    {
        try
        {
            Address address = _ctx.Adresses.Find(customer.AddressId);

            if (address == null) {
                return NotFound();
            }

            customer.CreatedAt = DateTime.UtcNow;
            customer.Address = address;

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            
            return Created("", customer);
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
            Customer? customer = 
                _ctx.Customers
                .Include(x => x.Address)
                .FirstOrDefault(x => x.Name == name);
            if (customer != null)
            {
                return Ok(customer);
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
            Customer? customer = _ctx.Customers.Find(id);

            if (customer != null)
            {
                _ctx.Customers.Remove(customer);
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
    public IActionResult Put([FromRoute] int id, [FromBody] Customer customer)
    {
        try
        {
            Address address = _ctx.Adresses.Find(customer.AddressId);

            if (address == null) {
                return NotFound();
            }

            Customer? customers = _ctx.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customers != null)
            {
                customers.Name = customer.Name;
                customers.CPF = customer.CPF;
                customers.Phone = customer.Phone;
                customers.Email = customer.Email;

                customers.Address = address;

                _ctx.Customers.Update(customers);
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