using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskCheck.Models;
using System;

namespace DeskCheck.Controllers
{
    [ApiController]
    [Route("desk")]
    public class DesksController : ControllerBase
    {
        private readonly DeskCheckContext _context;
        private List<Desk> desks;

        public DesksController(DeskCheckContext context)
        {
            desks = new List<Desk>();
            _context = context;
            
            for(int i = 0; i < 5; i++)
            {
                desks.Add(NewDesk(i));
            }
        }

        public Desk NewDesk(int id)
        {
            var rng = new Random();
            return new Desk
            {
                deskID = id,
                temperature = rng.Next(10, 30),
                CO2 = rng.Next(200, 500), // Parts Per Million (PPM)
                floor = rng.Next(0, 5),
                X = rng.Next(50, 90),
                Y = rng.Next(50, 90),
                registered = rng.Next(0, 2) == 1
            };
        }

        // GET: desk/getAll
        [HttpGet("getAll")]
        public List<Desk> GetDesks()
        {
            foreach(Desk d in desks){ Console.WriteLine(d.deskID); }
            return desks;
        }

        // GET: desk/getDesk
        [HttpGet("getDesk/{id}")]
        public ActionResult<Desk> GetDesk(int id)
        {
            foreach(var desk in desks){
                if(desk.deskID == id)
                {
                    return desk;
                }
            }

            return NotFound();
        }

        [HttpPut("add")]
        public void AddDesk()
        {
            int numdesks = desks.Count();
            int desknum = 0;

            if(numdesks > 0)
            {
                Desk ldesk = desks[numdesks - 1];
                desknum = ldesk.deskID;;
            }
            desknum++;
            desks.Add(NewDesk(desknum));
            foreach(Desk d in desks){ Console.WriteLine(d.deskID); }
            return;
        }

        // PUT: api/Desks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /*********************************
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesk(int id, Desk desk)
        {
            if (id != desk.deskID)
            {
                return BadRequest();
            }

            _context.Entry(desk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskExists(id))
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

        // POST: api/Desks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Desk>> PostDesk(Desk desk)
        {
            _context.Desks.Add(desk);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDesk), new { id = desk.deskID }, desk);
        }

        // DELETE: api/Desks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Desk>> DeleteDesk(int id)
        {
            var desk = await _context.Desks.FindAsync(id);
            if (desk == null)
            {
                return NotFound();
            }

            _context.Desks.Remove(desk);
            await _context.SaveChangesAsync();

            return desk;
        }

        private bool DeskExists(int id)
        {
            return _context.Desks.Any(e => e.deskID == id);
        }***********************************/
    }
}