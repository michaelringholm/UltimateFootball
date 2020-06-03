using System;

namespace dk.opusmagus.fd.dtl
{
    public class TeamDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LeagueId { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int MatchesPlayed { get; set; }
        public string Trend { get; set; }
        public int LeaguePosition { get; set; }
    }
}
