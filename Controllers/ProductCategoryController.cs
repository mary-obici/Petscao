using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
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
                
                if (products.Count == 0)
                {
                    return NotFound("Nenhuma categoria de produto encontrada.");
                }
                
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar categorias de produto: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] ProductCategory product)
        {
            try
            {
                product.CreatedAt = DateTime.UtcNow;
                _ctx.ProductCategories.Add(product);
                _ctx.SaveChanges();
                return Created("", product);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar categoria de produto: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                ProductCategory products = _ctx.ProductCategories.FirstOrDefault(x => x.Name == name);
                
                if (products != null)
                {
                    return Ok(products);
                }
                
                return NotFound($"Categoria de produto com o nome '{name}' não encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar categoria de produto: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                ProductCategory products = _ctx.ProductCategories.Find(id);
                
                if (products != null)
                {
                    _ctx.ProductCategories.Remove(products);
                    _ctx.SaveChanges();
                    return Ok();
                }
                
                return NotFound($"Categoria de produto com ID '{id}' não encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir categoria de produto: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ProductCategory product)
        {
            try
            {
                ProductCategory products = _ctx.ProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
                
                if (products != null)
                {
                    products.Name = product.Name;
                    products.Description = product.Description;
                    _ctx.ProductCategories.Update(products);
                    _ctx.SaveChanges();
                    return Ok();
                }
                
                return NotFound($"Categoria de produto com ID '{id}' não encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar categoria de produto: {e.Message}");
            }
        }
    }
}
