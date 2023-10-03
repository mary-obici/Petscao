using Petscao.Data;
using Petscao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
                    .ToList();

                return timelines.Count == 0 ? NotFound() : Ok(timelines);
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
                _ctx.Timeline.Add(timeline);
                _ctx.SaveChanges();

                return Created("", timeline);
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

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Timeline timeline)
        {
            try
            {
                Timeline? existingTimeline = _ctx.Timeline.FirstOrDefault(x => x.TimelineId == id);

                if (existingTimeline != null)
                {
                    existingTimeline.CustomerId = timeline.CustomerId;
                    existingTimeline.AnimalId = timeline.AnimalId;
                    existingTimeline.ServiceId = timeline.ServiceId;
                    existingTimeline.Date = timeline.Date;

                    _ctx.Timeline.Update(existingTimeline);
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
