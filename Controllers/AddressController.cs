using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;

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
            List<Address> adresses = _ctx.Adresses.ToList();
            return adresses.Count == 0 ? NotFound() : Ok(adresses);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getByStreet/{street}")]
    public IActionResult GetByStreet([FromRoute] string street)
    {
        try
        {
            Address? adresses = _ctx.Adresses.FirstOrDefault(x => x.Street == street);
            if (adresses != null)
            {
                return Ok(adresses);
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("put/{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] Address address)
    {
        try
        {
            Address? adresses = _ctx.Adresses.FirstOrDefault(x => x.AddressId == id);
            if (adresses != null)
            {
                adresses.Street = address.Street;
                adresses.Number = address.Number;
                adresses.City = address.City;
                adresses.Neighborhood = address.Neighborhood;
                adresses.CEP = address.CEP;

                _ctx.Adresses.Update(adresses);
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

    [HttpDelete]
    [Route("Delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            Address? adresses = _ctx.Adresses.Find(id);
            if (adresses != null)
            {
                _ctx.Adresses.Remove(adresses);
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