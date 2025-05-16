using Sporting.Data;
using Sporting.Models;
using Sporting.Repository.Interfacees;

namespace Sporting.Repository.Implementation
{
    public class LeagueServices : ILeagueServices
    {
        private readonly ApplicationDbContext _context;
        public LeagueServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<League> GetAll()
        {
            return _context.leagues.ToList();
        }

        public League GetbyId(int id)
        {
            League league = _context.leagues.FirstOrDefault(o=>o.Id == id);
            return league;
        }

        public void Add(League league)
        {
         _context.leagues.Add(league);   
            _context.SaveChanges();
        }

        public void Update(League league)
        {
            _context.leagues.Update(league);
            _context.SaveChanges();

        }
        public void Delete(League league)
        {
            _context.leagues.Remove(league);
            _context.SaveChanges();

        }






    }
}
