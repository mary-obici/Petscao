using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petscao.Data;
using Petscao.Models;

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

                return suppliers.Count == 0 ? NotFound() : Ok(suppliers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            try
            {
                _ctx.Suppliers.Add(supplier);
                _ctx.SaveChanges();

                return Created("", supplier);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                Supplier? supplier = _ctx.Suppliers
                    .Include(x => x.Address)
                    .FirstOrDefault(x => x.SupplierId == id);

                if (supplier != null)
                {
                    return Ok(supplier);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Supplier? supplier = _ctx.Suppliers.Find(id);

                if (supplier != null)
                {
                    _ctx.Suppliers.Remove(supplier);
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
        public IActionResult Put([FromRoute] int id, [FromBody] Supplier supplier)
        {
            try
            {
                Supplier? existingSupplier = _ctx.Suppliers.FirstOrDefault(x => x.SupplierId == id);

                if (existingSupplier != null)
                {
                    existingSupplier.CorporateReason = supplier.CorporateReason;
                    existingSupplier.FantasyName = supplier.FantasyName;
                    existingSupplier.CNPJ = supplier.CNPJ;
                    existingSupplier.Phone = supplier.Phone;
                    existingSupplier.Email = supplier.Email;
                    existingSupplier.AddressId = supplier.AddressId;

                    _ctx.Suppliers.Update(existingSupplier);
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
}
