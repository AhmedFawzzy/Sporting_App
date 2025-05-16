namespace Sporting.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }



    }
}
