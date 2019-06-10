using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationRestAPI;
//using WebApplicationRestAPI.Models;

namespace WebApplicationRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        public static AwardsContext _contextAward;

        public AwardsController(AwardsContext contextAward)
        {
            _contextAward = contextAward;
            if(!_contextAward.Awards.Any())
            {
                _contextAward.Awards.Add(new Award() { Title = "Emmy", Description = "Американская телевизионная премия" });
                _contextAward.Awards.Add(new Award() { Title = "Grammy", Description = "Музыкальная премия Американской академии звукозаписи" });
                _contextAward.Awards.Add(new Award() { Title = "Oskar", Description = "Премия Американской академии кинематографических искусств и наук" });
                _contextAward.Awards.Add(new Award() { Title = "Tony", Description = "Театральная премия" });
                _contextAward.Awards.Add(new Award() { Title = "Премия Дарвина", Description = "Неофициальная премия, присуждаемая комиссией людям лишившим себя наследства по своей глупости и неосторожности" });
                _contextAward.SaveChanges();
            }
        }

        // GET: api/Awards
        [HttpGet]
        public IEnumerable<Award> GetAwards()
        {
            return _contextAward.Awards;
        }

        // GET: api/Awards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAward([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var award = await _contextAward.Awards.FindAsync(id);

            if (award == null)
            {
                return NotFound();
            }

            return Ok(award);
        }

        // PUT: api/Awards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAward([FromRoute] int id, [FromBody] Award award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != award.Id)
            {
                return BadRequest();
            }

            _contextAward.Entry(award).State = EntityState.Modified;

            try
            {
                await _contextAward.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AwardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Awards
        [HttpPost]
        public async Task<IActionResult> PostAward([FromBody] Award award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _contextAward.Awards.Add(award);
            await _contextAward.SaveChangesAsync();

            return CreatedAtAction("GetAward", new { id = award.Id }, award);
        }

        // DELETE: api/Awards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var award = await _contextAward.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound();
            }

            _contextAward.Awards.Remove(award);
            await _contextAward.SaveChangesAsync();

            return Ok(award);
        }

        private bool AwardExists(int id)
        {
            return _contextAward.Awards.Any(e => e.Id == id);
        }
    }
}