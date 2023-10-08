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
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public EmployeeController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Employee> employees = _ctx.Employees
                    .Include(x => x.Address)
                    .ToList();

                if (employees.Count == 0)
                {
                    return NotFound("Nenhum funcionário encontrado.");
                }

                return Ok(employees);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar funcionários: {e.Message}");
            }
        }

        [HttpGet]
        [Route("getByString/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                Employee employee = _ctx.Employees
                    .Include(x => x.Address)
                    .FirstOrDefault(x => x.Name == name);

                if (employee != null)
                {
                    return Ok(employee);
                }

                return NotFound($"Funcionário com o nome '{name}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar funcionário: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                Address address = _ctx.Adresses.Find(employee.AddressId);

                if (address == null)
                {
                    return NotFound("Endereço não encontrado.");
                }

                employee.CreatedAt = DateTime.UtcNow;
                employee.Address = address;

                _ctx.Employees.Add(employee);
                _ctx.SaveChanges();

                return Created("", employee);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar funcionário: {e.Message}");
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Employee employee)
        {
            try
            {
                Address address = _ctx.Adresses.Find(employee.AddressId);

                if (address == null)
                {
                    return NotFound("Endereço não encontrado.");
                }

                Employee existingEmployee = _ctx.Employees.FirstOrDefault(x => x.EmployeeId == id);

                if (existingEmployee != null)
                {
                    existingEmployee.Name = employee.Name;
                    existingEmployee.CPF = employee.CPF;
                    existingEmployee.Phone = employee.Phone;
                    existingEmployee.Email = employee.Email;

                    existingEmployee.Address = address;

                    _ctx.Employees.Update(existingEmployee);
                    _ctx.SaveChanges();

                    return Ok();
                }

                return NotFound($"Funcionário com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar funcionário: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Employee employee = _ctx.Employees.Find(id);

                if (employee != null)
                {
                    _ctx.Employees.Remove(employee);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound($"Funcionário com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao excluir funcionário: {e.Message}");
            }
        }
    }
}
