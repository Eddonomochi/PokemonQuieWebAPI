using Identity.Dapper.Entities;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.CalculationsService
{
    public interface IQuizCalculations
    {
        double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted);
        Task<double> PrecentileFinder (int userID);
        Task<int> RankFinder(int userID);
    }
}
