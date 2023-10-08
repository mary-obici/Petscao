using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Service")]
    public class ServiceController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        public ServiceController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Service> services = _ctx.Services.ToList();

                if (services.Count == 0)
                {
                    return NotFound("Nenhum serviço encontrado.");
                }

                return Ok(services);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar serviços: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                Service services = _ctx.Services.FirstOrDefault(x => x.Name == name);

                if (services != null)
                {
                    return Ok(services);
                }

                return NotFound($"Serviço com o nome '{name}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar serviço: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Service service)
        {
            try
            {
                service.CreatedAt = DateTime.UtcNow;

                _ctx.Services.Add(service);
                _ctx.SaveChanges();
                return Created("", service);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar serviço: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Service service)
        {
            try
            {
                Service services = _ctx.Services.FirstOrDefault(x => x.ServiceId == id);

                if (services != null)
                {
                    services.Name = service.Name;
                    services.Description = service.Description;
                    services.Code = service.Code;
                    services.UnitPrice = service.UnitPrice;

                    _ctx.Services.Update(services);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Serviço com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar serviço: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Service services = _ctx.Services.Find(id);

                if (services != null)
                {
                    _ctx.Services.Remove(services);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Serviço com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir serviço: {e.Message}");
            }
        }
    }
}
