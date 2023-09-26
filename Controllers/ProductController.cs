using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public ProductController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    [Route("getAll")]
    public IActionResult GetAll()
    {
        try
        {
            List<Product> products = _ctx.Products.ToList();
            return products.Count == 0 ? NotFound() : Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("post")]
    public IActionResult Post([FromBody] Product product)
    {
        Guid guid = Guid.NewGuid();
        // Console.WriteLine(guid.ToString());
        try
        {
            product.ProductCategory = _ctx.ProductCategories.Find(product.IdProductCategory);
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return Created("", product);
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
            Product? products = _ctx.Products.FirstOrDefault(x => x.Name == name);
            if (products != null)
            {
                return Ok(products);
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
            Product? products = _ctx.Products.Find(id);
            if (products != null)
            {
                _ctx.Products.Remove(products);
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
    public IActionResult Put([FromRoute] int id, [FromBody] Product product)
    {
        try
        {
            Product? products = _ctx.Products.FirstOrDefault(x => x.IdProduct == id);
            if (products != null)
            {
                products.Name = product.Name;
                products.Description = product.Description;
                products.UnitPrice = product.UnitPrice;
                products.Amount = product.Amount;
                _ctx.Products.Update(products);
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