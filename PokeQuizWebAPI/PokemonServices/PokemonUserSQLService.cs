using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonDAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonUserSQLService : IPokemonUserSQLService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;
        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PokemonUserSQLService(IPokemonUserSQLStore pokemonUserSQLStore, UserManager<DapperIdentityUser> userManager, IHttpContextAccessor httpsContextAccessor)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
            _userManager = userManager;
            _httpContextAccessor = httpsContextAccessor;
        }


        public async Task CreatePokemonUserData(QuizAttemptResultsViewModel model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var dalModel = new PokemonDALModel();
            var pokePlayer = await _pokemonUserSQLStore.GetUserScoreData(user.Id);

            if (user.Id == pokePlayer?.FK_UsernameID)
            {
                pokePlayer.TotalAccumlatiedPoints += model.AmountCorrect;
                pokePlayer.TotalPossiblePoints += model.QuestionsAttempted;
                pokePlayer.OverallPercent = Convert.ToSingle(pokePlayer.TotalAccumlatiedPoints) / Convert.ToSingle(pokePlayer.TotalPossiblePoints);
                pokePlayer.RecentTotalCorrect = model.AmountCorrect;
                pokePlayer.RecentAmountOfQuestions = model.QuestionsAttempted;
                pokePlayer.WhichQuizTaken = model.QuestionsAttempted.ToString();
                pokePlayer.AttemptsPerQuiz += 1;
                await _pokemonUserSQLStore.UpdateUserStatusAtQuizEnd(pokePlayer);
            }
            else
            {
                dalModel.Username = user.UserName;
                dalModel.FK_UsernameID = user.Id;
                dalModel.TotalAccumlatiedPoints += model.AmountCorrect;
                dalModel.TotalPossiblePoints += model.QuestionsAttempted;
                dalModel.OverallPercent = Convert.ToSingle(dalModel.TotalAccumlatiedPoints) / Convert.ToSingle(dalModel.TotalPossiblePoints);
                dalModel.RecentTotalCorrect = model.AmountCorrect;
                dalModel.RecentAmountOfQuestions = model.QuestionsAttempted;
                dalModel.WhichQuizTaken = model.QuestionsAttempted.ToString();
                dalModel.AttemptsPerQuiz += 1;
                await _pokemonUserSQLStore.InsertUserStatusAtQuizEnd(dalModel);
            }
        }

        public async Task<double> ReturnPlayersAveragePercent()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userAverageScore = await  _pokemonUserSQLStore.SelectPlayerAverageScore(user.Id);
            return userAverageScore;
        }

        public async Task<IEnumerable<double>> SelectAllScores()
        {
            var averageScores = await _pokemonUserSQLStore.SelectAllScores();
            return averageScores;
        }
    }
}
