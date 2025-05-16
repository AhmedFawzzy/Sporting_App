using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sporting.Models.LeagueModels;
using Sporting.Repository.Implementation;
using static Sporting.Models.LeagueModels.RootResponse;

namespace Sporting.Controllers
{

    public class FootballController : Controller
        {
            private readonly FootballApiService _apiService;

            public FootballController(FootballApiService apiService)
            {
                _apiService = apiService;
            }

        public async Task<IActionResult> Leagues()
        {
            List<LeagueResponse> leagues = await _apiService.GetLeaguesAsync();
            return View(leagues);
        }
        public async Task<IActionResult> TopScorers(int? leagueId, int? season)
        {
            // Set default values if none selected
            int selectedLeague = leagueId ?? 39;  // Premier League
            int selectedSeason = season ?? 2023;

            // Get top scorers for selected league and season
            var topScorers = await _apiService.GetTopScorersAsync(selectedLeague, selectedSeason);

            // Get sorted leagues for dropdown
            var leagues = await _apiService.GetSortedLeaguesAsync();

            // Generate a season list from 2010 to current year
            var seasons = Enumerable.Range(2010, DateTime.Now.Year - 2009).Reverse().ToList();

            // Pass to ViewBag for dropdowns
            ViewBag.Leagues = new SelectList(leagues.Select(l => new {
                Id = l.League.Id,
                Name = $"{l.League.Name} ({l.Country.Name})"
            }), "Id", "Name", selectedLeague);

            ViewBag.Seasons = new SelectList(seasons, selectedSeason);

            return View(topScorers);
        }



    }



}
