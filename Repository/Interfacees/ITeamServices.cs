using Sporting.Models;

namespace Sporting.Repository.Interfacees
{
    public interface ITeamServices
    {
        List<Team> GetAll();
        Team GetbyId(int id);
        List<Team> GetTeamsByLeague(int LeagueId);
        void Add(Team league);
        void Update(Team league);
        void Delete(Team league);
    }
}
