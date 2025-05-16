using Sporting.Models;
using System.Diagnostics.Metrics;

namespace Sporting.Models.LeagueModels
{
    public class LeagueResponse
    {
        public League League { get; set; }
        public Country Country { get; set; }
        public List<Season> Seasons { get; set; }
    }
}
