using PokeQuizWebAPI.Models.QuizModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IPokemonUserSQLService
    {
         Task CreatePokemonUserData(QuizAttemptResultsViewModel model);
         Task<IEnumerable<double>> SelectAllScores();
         Task<double> ReturnPlayersAveragePercent();
    }


}

