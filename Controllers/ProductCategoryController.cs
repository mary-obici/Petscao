using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/productCategory")]
public class ProductCategoryController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public ProductCategoryController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    [Route("getAll")]
    public IActionResult GetAll()
    {
        try
        {
            List<ProductCategory> products = _ctx.ProductCategories.ToList();
            return products.Count == 0 ? NotFound() : Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("post")]
    public IActionResult Post([FromBody] ProductCategory product)
    {
        Guid guid = Guid.NewGuid();
        try
        {
            _ctx.ProductCategories.Add(product);
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
            ProductCategory? products = _ctx.ProductCategories.FirstOrDefault(x => x.Name == name);
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
            ProductCategory? products = _ctx.ProductCategories.Find(id);
            if (products != null)
            {
                _ctx.ProductCategories.Remove(products);
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
    public IActionResult Put([FromRoute] int id, [FromBody] ProductCategory product)
    {
        try
        {
            ProductCategory? products = _ctx.ProductCategories.FirstOrDefault(x => x.IdProductCategory == id);
            if (products != null)
            {
                products.Name = product.Name;
                products.Description = product.Description;
                _ctx.ProductCategories.Update(products);
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