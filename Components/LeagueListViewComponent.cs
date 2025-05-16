using Microsoft.AspNetCore.Mvc;
using Sporting.Repository.Interfacees;

namespace Sporting.Components
{
    public class LeagueListViewComponent : ViewComponent
    {
        private readonly ILeagueServices _leagueServices;

        public LeagueListViewComponent(ILeagueServices leagueServices)
        {
            _leagueServices = leagueServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var leagues = _leagueServices.GetAll();
            return View(leagues);
        }
    }
}