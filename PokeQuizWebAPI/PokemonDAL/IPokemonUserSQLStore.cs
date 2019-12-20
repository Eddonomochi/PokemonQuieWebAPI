using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonDAL
{
    public interface IPokemonUserSQLStore
    {
        Task<bool> UpdateUserStatusAtQuizEnd(PokemonDALModel dalModel);
        Task<bool> InsertUserStatusAtQuizEnd(PokemonDALModel dalModel);

        Task<PokemonDALModel> GetUserScoreData(int userID);
        Task<IEnumerable<double>> SelectAllScores();
        Task<double> SelectPlayerAverageScore(int id);
        Task<IEnumerable<string>> SelectOrderedPlayers(int topNumb);
    }
}
