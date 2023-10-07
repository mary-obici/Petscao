using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Timeline")]
    public class TimelineController : ControllerBase
    {
        private readonly AppDataContext _ctx;
        public TimelineController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Timeline> timelines = _ctx.Timeline
                    .Include(x => x.Customer)
                    .Include(x => x.Animal)
                    .Include(x => x.Service)
                    .Include(x => x.Employee)
                    .ToList();

                return timelines.Count == 0 ? NotFound("Não existe agendamentos!") : Ok(timelines);
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
                Timeline? timeline = _ctx.Timeline
                    .Include(x => x.Customer)
                    .Include(x => x.Animal)
                    .Include(x => x.Service)
                    .Include(x => x.Employee)
                    .FirstOrDefault(x => x.TimelineId == id);

                if (timeline != null)
                {
                    return Ok(timeline);
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
        public IActionResult Post([FromBody] Timeline timeline)
        {
            try
            {
                Customer customer = _ctx.Customers.Find(timeline.CustomerId);

                if (customer == null)
                {
                    return NotFound("O cliente informado não existe!");
                }

                Animal animal = _ctx.Animals.Find(timeline.AnimalId);

                if (animal == null)
                {
                    return NotFound("O animal informado não existe!");
                }

                if (animal.CustomerId != timeline.CustomerId)
                {
                    return BadRequest("O animal não pertence ao cliente especificado.");
                }

                Service service = _ctx.Services.Find(timeline.ServiceId);

                if (service == null)
                {
                    return NotFound("O serviço informado não existe");
                }

                Employee employee = _ctx.Employees.Find(timeline.EmployeeId);

                if (employee == null)
                {
                    return NotFound("O funcionário informado não existe");
                }

                //select agendamentos where idEmployee == recebido request 
                List <Timeline> agendamentosInDb = _ctx.Timeline
                    .Where(timelineAgendadas => timelineAgendadas.EmployeeId == timeline.EmployeeId)
                    .ToList();

                    //14h >= 14h && 14h <= 14:30 || 
                    // 13:30 >= 14h && 13:30 <= 14:30 
                    // 14:30 >= 14h && 14:30 <= 14:30

                    bool newTime = agendamentosInDb.Any(registro =>
                        (timeline.StartDate >= registro.StartDate && timeline.StartDate <= registro.EndDate) ||
                        (timeline.EndDate >= registro.StartDate && timeline.EndDate <= registro.EndDate));

                    if (newTime == true ) {
                        return BadRequest("Horário ocupado! Altere para um disponível");
                    }

                //verifica null, alterar o que recebe para pt-br 
                DateTime? startDate = DateTime.ParseExact(timeline.StartDate?.ToString("dd/MM/yyyy HH:mm") ??
                    "01/01/2000 00:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                DateTime? endDate = DateTime.ParseExact(timeline.EndDate?.ToString("dd/MM/yyyy HH:mm") ??
                    "01/01/2000 00:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                if (startDate >= endDate)
                {
                    return BadRequest("A data de início deve ser anterior à data de término.");
                }

                timeline.CreatedAt = DateTime.UtcNow;
                timeline.Customer = customer;
                timeline.Animal = animal;
                timeline.Service = service;
                timeline.Employee = employee;
                timeline.StartDate = startDate;
                timeline.EndDate = endDate;

                _ctx.Timeline.Add(timeline);
                _ctx.SaveChanges();

                return Created("Agendamento criado!", timeline);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Timeline timeline)
        {
            try
            {
                Customer customer = _ctx.Customers.Find(timeline.CustomerId);

                if (customer == null)
                {
                    return NotFound("O cliente informado não existe!");
                }

                Animal animal = _ctx.Animals.Find(timeline.AnimalId);

                if (animal == null)
                {
                    return NotFound("O animal informado não existe!");
                }

                Service service = _ctx.Services.Find(timeline.ServiceId);

                if (service == null)
                {
                    return NotFound("O serviço informado não existe!");
                }

                Employee employee = _ctx.Employees.Find(timeline.EmployeeId);

                if (employee == null)
                {
                    return NotFound("O funcionario informado não existe!");
                }

                //select agendamentos where idEmployee == recebido request 
                List <Timeline> agendamentosInDb = _ctx.Timeline
                    .Where(timelineAgendadas => timelineAgendadas.EmployeeId == timeline.EmployeeId)
                    .ToList();

                    //14h >= 14h && 14h <= 14:30 || 
                    // 13:30 >= 14h && 13:30 <= 14:30 
                    // 14:30 >= 14h && 14:30 <= 14:30

                bool newTime = agendamentosInDb.Any(registro =>
                    (timeline.StartDate >= registro.StartDate && timeline.StartDate <= registro.EndDate) ||
                    (timeline.EndDate >= registro.StartDate && timeline.EndDate <= registro.EndDate));


                //verifica null, alterar o que recebe para pt-br 
                DateTime? startDate = DateTime.ParseExact(timeline.StartDate?.ToString("dd/MM/yyyy HH:mm") ??
                    "01/01/2000 00:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                DateTime? endDate = DateTime.ParseExact(timeline.EndDate?.ToString("dd/MM/yyyy HH:mm") ??
                    "01/01/2000 00:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                if (startDate >= endDate)
                {
                    return BadRequest("A data de início deve ser anterior à data de término.");
                }

                Timeline? existingTimeline = _ctx.Timeline.FirstOrDefault(x => x.TimelineId == id);

                if (existingTimeline != null)
                {
                    existingTimeline.Customer = customer;
                    existingTimeline.Animal = animal;
                    existingTimeline.Service = service;
                    existingTimeline.Employee = employee;
                    existingTimeline.StartDate = startDate;
                    existingTimeline.EndDate = endDate;

                    _ctx.Timeline.Update(existingTimeline);
                    _ctx.SaveChanges();

                    return Ok();
                }

                return NotFound("Não foi possível alterar o agendamento!");
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
                Timeline? timeline = _ctx.Timeline.Find(id);

                if (timeline != null)
                {
                    _ctx.Timeline.Remove(timeline);
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
