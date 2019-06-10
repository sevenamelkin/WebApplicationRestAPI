using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloWebApi.Models;
using WebApplicationRestAPI;

namespace WebApplicationRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _contextUser;

        public UsersController(UsersContext contextUser)
        {
            _contextUser = contextUser;
            if (!_contextUser.Users.Any())
            {
                _contextUser.Users.Add(new User() { Name = "Денис", LastName = "Евдокименко", BirthDate = new DateTime(2000, 11, 08), Listawards = new List<Award> { /*AwardsController._contextAward.Awards[1]*/} });
                _contextUser.Users.Add(new User() { Name = "Герман", LastName = "Алтуфьев", BirthDate = new DateTime(2004, 03, 18), Listawards = new List<Award> { } });
                _contextUser.Users.Add(new User() { Name = "Евгений", LastName = "Парфенов", BirthDate = new DateTime(1994, 01, 01), Listawards = new List<Award> { } });
                _contextUser.Users.Add(new User() { Name = "Петр", LastName = "Костеров", BirthDate = new DateTime(1995, 10, 08), Listawards = new List<Award> { } });
                _contextUser.Users.Add(new User() { Name = "Семён", LastName = "Амелькин", BirthDate = new DateTime(1998, 02, 19), Listawards = new List<Award> { } });
                _contextUser.Users.Add(new User() { Name = "Игорь", LastName = "Василенко", BirthDate = new DateTime(1995, 12, 03), Listawards = new List<Award> { } });
                _contextUser.Users.Add(new User() { Name = "Влад", LastName = "Акумов", BirthDate = new DateTime(1990, 04, 22), Listawards = new List<Award> { } });
                _contextUser.Users.Add(new User() { Name = "Виталий", LastName = "Астахов", BirthDate = new DateTime(1993, 05, 01), Listawards = new List<Award> { } });
                _contextUser.SaveChanges();
            }
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _contextUser.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _contextUser.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _contextUser.Entry(user).State = EntityState.Modified;

            try
            {
                await _contextUser.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _contextUser.Users.Add(user);
            await _contextUser.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _contextUser.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _contextUser.Users.Remove(user);
            await _contextUser.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _contextUser.Users.Any(e => e.Id == id);
        }
    }
}