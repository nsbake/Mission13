using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository repo { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBowlersRepository temp)
        {
            _logger = logger;
            repo = temp;
        }

        public IActionResult Index()
        {
            ViewBag.bowlers = repo.Bowlers.Include(x => x.Team).ToList();

            return View();
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            var bowler = new Bowler();
            bowler.BowlerID = repo.Bowlers.Max(x => x.BowlerID) + 1;
            ViewBag.listTeams = repo.Teams.ToList();
            ViewBag.type = "Add";
            

            return View("Bowling", bowler);
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            repo.AddBowler(b);
            var type = "Add";

            return View("Confirmation", type);
        }

        [HttpGet]
        public IActionResult EditBowler(int id)
        {
            var b = repo.Bowlers.Where(x => x.BowlerID == id).First();
            ViewBag.listTeams = repo.Teams.ToList();
            ViewBag.type = "Edit";


            return View("Bowling", b);
        }

        [HttpPost]
        public IActionResult EditBowler(Bowler b)
        {
            repo.EditBowler(b);
            var type = "Edit";

            return View("Confirmation", type);
        }

        [HttpGet]
        public IActionResult DeleteBowler(int id)
        {
            var b = repo.Bowlers.Where(x => x.BowlerID == id).First();

            repo.DeleteBowler(b);

            return RedirectToAction("Index");
        }
    }
}