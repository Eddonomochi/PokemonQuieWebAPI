using PokeQuizWebAPI.Models.PlayerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PlayerServices
{
    public interface IPlayerService
    {
        Task<IEnumerable<string>> SelectTopTenPlayers();
        Task<PlayerRankModel> AssemblePlayerRank();
    }
}
