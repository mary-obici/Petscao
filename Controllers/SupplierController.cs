using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petscao.Data;
using Petscao.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Supplier")]
    public class SupplierController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public SupplierController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Supplier> suppliers = _ctx.Suppliers
                    .Include(x => x.Address)
                    .ToList();

                if (suppliers.Count == 0)
                {
                    return NotFound("Nenhum fornecedor encontrado.");
                }

                return Ok(suppliers);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar fornecedores: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByString/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                Supplier supplier = _ctx.Suppliers
                    .Include(x => x.Address)
                    .FirstOrDefault(x => x.FantasyName == name);

                if (supplier != null)
                {
                    return Ok(supplier);
                }

                return NotFound($"Fornecedor com o nome '{name}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar fornecedor: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            try
            {
                Address address = _ctx.Adresses.Find(supplier.AddressId);

                if (address == null)
                {
                    return NotFound("Endereço não encontrado.");
                }

                supplier.CreatedAt = DateTime.UtcNow;
                supplier.Address = address;

                _ctx.Suppliers.Add(supplier);
                _ctx.SaveChanges();

                return Created("", supplier);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar fornecedor: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Supplier supplier)
        {
            try
            {
                Address address = _ctx.Adresses.Find(supplier.AddressId);

                if (address == null)
                {
                    return NotFound("Endereço não encontrado.");
                }

                Supplier existingSupplier = _ctx.Suppliers.FirstOrDefault(x => x.SupplierId == id);

                if (existingSupplier != null)
                {
                    existingSupplier.CorporateReason = supplier.CorporateReason;
                    existingSupplier.FantasyName = supplier.FantasyName;
                    existingSupplier.CNPJ = supplier.CNPJ;
                    existingSupplier.Phone = supplier.Phone;
                    existingSupplier.Email = supplier.Email;

                    existingSupplier.Address = address;

                    _ctx.Suppliers.Update(existingSupplier);
                    _ctx.SaveChanges();

                    return Ok();
                }

                return NotFound($"Fornecedor com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar fornecedor: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Supplier supplier = _ctx.Suppliers.Find(id);

                if (supplier != null)
                {
                    _ctx.Suppliers.Remove(supplier);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Fornecedor com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir fornecedor: {e.Message}");
            }
        }
    }
}
