using static Sporting.Models.LeagueModels.RootResponse;
using System.Text.Json;
using Sporting.Models.LeagueModels;
using Sporting.Models.TopSoccerModels;

namespace Sporting.Repository.Implementation
{
    public class FootballApiService
    {
        private readonly HttpClient _httpClient;

        public FootballApiService(HttpClient httpClient, IConfiguration config)
        {
            string baseUrl = config["ApiFootball:BaseUrl"];
            string apiKey = config["ApiFootball:ApiKey"];

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Add("x-apisports-key", apiKey);
        }

        public async Task<List<LeagueResponse>> GetLeaguesAsync()
        {
            var response = await _httpClient.GetAsync("leagues");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var leagues = JsonSerializer.Deserialize<RootResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return leagues?.Response ?? new List<LeagueResponse>();
        }
        public async Task<List<TopScorer>> GetTopScorersAsync(int leagueId, int season)
        {
            var response = await _httpClient.GetAsync($"players/topscorers?league={leagueId}&season={season}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TopScorersResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Response ?? new List<TopScorer>();
        }
        public async Task<List<LeagueResponse>> GetSortedLeaguesAsync()
        {
            var response = await _httpClient.GetAsync("leagues");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<RootResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.Response?
                .Where(l => l.League.Type == "League") // Optional: filter out cups
                .OrderBy(l => l.League.Name)
                .ToList() ?? new List<LeagueResponse>();
        }


    }


}
