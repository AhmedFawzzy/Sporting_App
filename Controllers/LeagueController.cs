using Microsoft.AspNetCore.Mvc;
using Sporting.Models;
using Sporting.Repository.Interfacees;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Sporting.Controllers
{
    public class LeagueController : Controller
    { 
        private readonly ITeamServices _Teamservices;
        private readonly ILeagueServices _Leagueservices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LeagueController(ITeamServices Teamservices, ILeagueServices Leagueservices,
            IWebHostEnvironment webHostEnvironment)
        {
            _Teamservices = Teamservices;
            _Leagueservices = Leagueservices;
            _webHostEnvironment = webHostEnvironment;
            

        }
        public IActionResult GetLeagues()
        {

            var leagues =_Leagueservices.GetAll();
             return View(leagues);
         }
    //public IActionResult  GetLeagues()
    //{
    //    var League = _Leagueservices.GetAll();
    //    return View(League);
    //}

    public IActionResult GetTeamsByLeague(int LeagueId)
        {
            var teams = _Teamservices.GetTeamsByLeague(LeagueId);
            return View(teams);
        }


        [HttpGet]

        public IActionResult CreateLeague()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLeague(League league, IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "leagues", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(fileStream);
                }

                league.Logo = "/images/leagues/" + uniqueFileName;
            }

            _Leagueservices.Add(league);
            return RedirectToAction("GetLeagues");
        }


        [HttpGet]
        public IActionResult UpdateLeague(int id)
        {
            var league = _Leagueservices.GetbyId(id);
            return View(league);
        }

        [HttpPost]
        public IActionResult UpdateLeague(League league, IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                // Delete old logo if exists
                if (!string.IsNullOrEmpty(league.Logo))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, league.Logo.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "leagues", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(fileStream);
                }

                league.Logo = "/images/leagues/" + uniqueFileName;
            }

            _Leagueservices.Update(league);
            return RedirectToAction("GetLeagues");
        }

       

        

        [HttpGet]
        public IActionResult DeleteLeague(int id)
        {
            var league = _Leagueservices.GetbyId(id);
            return View(league);
        }

        [HttpPost,ActionName("DeleteLeague")]
        public IActionResult RemoveLeague(int id)
        {
            var league = _Leagueservices.GetbyId(id);
            _Leagueservices.Delete(league);
            return RedirectToAction("GetLeagues");
        }

    }
}
