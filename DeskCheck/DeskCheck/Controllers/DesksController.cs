using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskCheck.Models;
using System;
using Newtonsoft.Json;
using System.IO;

namespace DeskCheck.Controllers
{
    [ApiController]
    [Route("desk")]
    public class DesksController : ControllerBase
    {
        private readonly DeskCheckContext _context;
        private List<Desk> desks;
        private string desksfpath;

        public DesksController(DeskCheckContext context)
        {
            _context = context;
            desksfpath= @".\DeskJsons\desks.json";
            using (StreamReader sr = new StreamReader(desksfpath))
            {
                string json = sr.ReadToEnd();
                desks = JsonConvert.DeserializeObject<List<Desk>>(json);
            }
            var rng = new Random();
            foreach(Desk d in desks)
            {
                d.CO2 += rng.Next(-100, 100);
                d.temp += rng.Next(-5, 5);
            }
        }

        public Desk NewDesk(int dnum)
        {
            var rng = new Random();
            Desk d = new Desk
            {
                deskID = dnum,
                temp = rng.Next(15, 25),
                CO2 = rng.Next(200, 500), // Parts Per Million (PPM)
                floor = rng.Next(0, 5),
                X = rng.Next(1, 90),
                Y = rng.Next(1, 90),
            };
            return d;
        }

        // GET: desk/getAll
        [HttpGet("getAll")]
        public List<Desk> GetDesks()
        {
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
            int numdesks = desks.Count;
            int dnum = 0;
            if (numdesks > 0)
            {
                dnum = desks[numdesks - 1].deskID;
                dnum++;
            }

            using StreamReader sr = new StreamReader(desksfpath);
            List<Desk> list;
            {
                string json = sr.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<Desk>>(json);
                list.Add(NewDesk(dnum));
                sr.Close();
            }

            using StreamWriter file = System.IO.File.CreateText(desksfpath);
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, list);    
            }
            
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
