using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskCheck.Models;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeskCheck.Controllers
{
    [ApiController]
    [Route("map")]
    public class MapsController : Controller
    {
        private readonly DeskCheckContext _context;
        public MapsController(DeskCheckContext context)
        {
            _context = context;
        }

        public String[] getMaps()
        {
            string dirpath = @".\MapJsons";
            String[] maps = Directory.GetFiles(dirpath);
            return maps;
        }

        [HttpGet("getSingle/{fnum}")]
        public ActionResult<string> GetSingle(int fnum)
        {
            String[] maps = getMaps();

            foreach (string name in maps)
            {
                if (name.Contains(fnum.ToString()))
                {
                    using StreamReader r = new StreamReader(name);
                    string json = r.ReadToEnd();
                    return json;
                }

            }
            return NotFound();
        }
    }
}
