using Sporting.Data;
using Sporting.Models;
using Sporting.Repository.Interfacees;

namespace Sporting.Repository.Implementation
{
    public class TeamServices : ITeamServices
    {
        private readonly ApplicationDbContext _context;
        public TeamServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        public Team GetbyId(int id)
        {
            Team? Team = _context.Teams.FirstOrDefault(o => o.Id == id);
            return Team;
        }

        public List<Team> GetTeamsByLeague(int LeagueId)
        {
            List<Team> Teams = _context.Teams.Where(o => o.LeagueId == LeagueId).ToList();
            return Teams;
        }
            



        public void Add(Team Team)
        {
            _context.Teams.Add(Team);
            _context.SaveChanges();
        }

        public void Update(Team Team)
        {
            _context.Teams.Update(Team);
            _context.SaveChanges();

        }
        public void Delete(Team Team)
        {
            _context.Teams.Remove(Team);
            _context.SaveChanges();

        }

    }
}
