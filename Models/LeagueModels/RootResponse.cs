using System.Net.NetworkInformation;

namespace Sporting.Models.LeagueModels
{
    public class RootResponse
    {
        public string Get { get; set; }
        public List<object> Parameters { get; set; }
        public List<object> Errors { get; set; }
        public int Results { get; set; }
        public Paging Paging { get; set; }
        public List<LeagueResponse> Response { get; set; }
    }
}
