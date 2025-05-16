using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sporting.Models;
using Sporting.Repository.Implementation;
using Sporting.Repository.Interfacees;

namespace Sporting.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamServices _Teamservices;
        private readonly ILeagueServices _leagueServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamController(ITeamServices Teamservices, ILeagueServices leagueServices, IWebHostEnvironment webHostEnvironment)
        {
            _Teamservices = Teamservices;
            _leagueServices = leagueServices;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetTeams()
        {
            var team = _Teamservices.GetAll();
            return View(team);
        }
        
        [HttpGet]
        public IActionResult GetTeamsByLeague(int LeagueId)
        {
            var teams = _Teamservices.GetTeamsByLeague(LeagueId);
            return View(teams);
        }

        [HttpGet]

        public IActionResult CreateTeam()
        {
            ViewBag.Leagues = _leagueServices.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeam(Team Team, IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Teams", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(fileStream);
                }

                Team.Logo = "/images/Teams/" + uniqueFileName;
            }

            _Teamservices.Add(Team);
            return RedirectToAction("GetTeams");
        }


        [HttpGet]
        public IActionResult UpdateTeam(int id)
        {
            var Team = _Teamservices.GetbyId(id);
            return View(Team);
        }

        [HttpPost]
        public IActionResult UpdateTeam(Team Team, IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                // Delete old logo if exists
                if (!string.IsNullOrEmpty(Team.Logo))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, Team.Logo.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Teams", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(fileStream);
                }

                Team.Logo = "/images/Teams/" + uniqueFileName;
            }

            _Teamservices.Update(Team);
            return RedirectToAction("GetTeams");
        }





        [HttpGet]
        public IActionResult DeleteTeam(int id)
        {
            var Team = _Teamservices.GetbyId(id);
            return View(Team);
        }

        [HttpPost, ActionName("DeleteTeam")]
        public IActionResult RemoveTeam(int id)
        {
            var Team = _Teamservices.GetbyId(id);
            _Teamservices.Delete(Team);
            return RedirectToAction("GetTeams");
        }



    }
}
