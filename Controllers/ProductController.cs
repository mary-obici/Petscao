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
                List<Product> products = 
                    _ctx.Products
                    .Include(x => x.Supplier)
                    .Include(x => x.ProductCategory)
                    .ToList();

                if (products.Count == 0)
                {
                    return NotFound("Nenhum produto encontrado.");
                }

                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar produtos: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                Supplier supplier = _ctx.Suppliers.Find(product.SupplierId);

                if (supplier == null)
                {
                    return NotFound("Fornecedor não encontrado.");
                }

                ProductCategory productCategory = _ctx.ProductCategories.Find(product.ProductCategoryId);

                if (productCategory == null)
                {
                    return NotFound("Categoria de produto não encontrada.");
                }

                product.CreatedAt = DateTime.UtcNow;
                product.Supplier = supplier;
                product.ProductCategory = productCategory;

                _ctx.Products.Add(product);
                _ctx.SaveChanges();
                return Created("", product);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar produto: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByCode/{code}")]
        public IActionResult GetByCode([FromRoute] string code)
        {
            try
            {
                Product products = _ctx.Products
                    .Include(x => x.Supplier)
                    .Include(x => x.ProductCategory)
                    .FirstOrDefault(x => x.Code == code);

                if (products != null)
                {
                    return Ok(products);
                }

                return NotFound($"Produto com o código '{code}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar produto: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Product products = _ctx.Products.Find(id);

                if (products != null)
                {
                    _ctx.Products.Remove(products);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Produto com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir produto: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                Supplier supplier = _ctx.Suppliers.Find(product.SupplierId);

                if (supplier == null)
                {
                    return NotFound("Fornecedor não encontrado.");
                }

                ProductCategory productCategory = _ctx.ProductCategories.Find(product.ProductCategoryId);

                if (productCategory == null)
                {
                    return NotFound("Categoria de produto não encontrada.");
                }

                Product products = _ctx.Products.FirstOrDefault(x => x.ProductId == id);

                if (products != null)
                {
                    products.Name = product.Name;
                    products.Description = product.Description;
                    products.UnitPrice = product.UnitPrice;
                    products.Amount = product.Amount;

                    products.Supplier = supplier;
                    products.ProductCategory = productCategory;

                    _ctx.Products.Update(products);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Produto com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar produto: {e.Message}");
            }
        }
    }
}
