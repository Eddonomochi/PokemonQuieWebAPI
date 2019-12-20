using System.Collections.Generic;

namespace PokeQuizWebAPI.Models.PlayerModels
{
    public class PlayerRankModel
    {
        public List<string> TopTenPlayers { get; set; }
        public double AverageScore { get; set; }
        public double PlayerPerrcentile { get; set; }
        public int PlayerRank { get; set; }
        public string Username { get; set; }

    }
}
