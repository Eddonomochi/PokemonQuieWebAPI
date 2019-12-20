
using Identity.Dapper.Entities;
using PokeQuizWebAPI.PokemonDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.CalculationsService
{
    public class QuizCalculations : IQuizCalculations
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;

        public QuizCalculations(IPokemonUserSQLStore pokemonUserSQLStore)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
        }
        public double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted)
        {
            var percentScoreThisAttempt = 0.0;
            var amountCorrect = Convert.ToDouble(questionsCorrect);
            var amountAttempted = Convert.ToDouble(questionsAttempted);

            percentScoreThisAttempt = (amountCorrect / amountAttempted) * 100;

            return percentScoreThisAttempt;
        }

        public async Task<double> PrecentileFinder(int userID)
        {
            var currentUserScore = await _pokemonUserSQLStore.SelectPlayerAverageScore(userID);
            var listOfUsers = await _pokemonUserSQLStore.SelectAllScores();
            var numOfBottomPrecentile = 0;
            //var userCount = listOfUsers.Count();
            var listOfUsersInList = new List<double>();
            var userCount = listOfUsersInList.Count();

            foreach (var allPlayerScores in listOfUsers)
            {
                listOfUsersInList.Add(allPlayerScores);
            }

            foreach (var allPlayersScoress in listOfUsersInList)
            {


                if (allPlayersScoress < currentUserScore)

                {
                    numOfBottomPrecentile += 1;
                }


            }

            var userPrecentile = (1d - (Convert.ToDouble(numOfBottomPrecentile) / Convert.ToDouble(userCount)));

            return userPrecentile;
        }

        public async Task<int> RankFinder(int userID)
        {
            var currentUserScore = await _pokemonUserSQLStore.SelectPlayerAverageScore(userID);
            var listOfUsers = await _pokemonUserSQLStore.SelectAllScores();
            var numOfBottomPrecentile = 0;
            var userCount = listOfUsers.Count();

            foreach (var allPlayersScores in listOfUsers)
            {
                {
                    if (allPlayersScores < currentUserScore)
                    {
                        numOfBottomPrecentile += 1;
                    }
                }
            }
            return userCount-numOfBottomPrecentile;
        }


    }
}
