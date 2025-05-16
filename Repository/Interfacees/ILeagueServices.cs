using Sporting.Models;

namespace Sporting.Repository.Interfacees
{
    public interface ILeagueServices
    {
        List<League> GetAll();
        League GetbyId(int id);
        void Add (League league);
        void Update (League league);
        void Delete (League league);
    }
}
