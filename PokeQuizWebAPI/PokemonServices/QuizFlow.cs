﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PokeQuizWebAPI.Models.QuizModels;

namespace PokeQuizWebAPI.PokemonServices
{
    public class QuizFlow : IQuizFlow
    {
        private readonly ISession _session;
        private readonly IRandomizer _randomizer;
        private readonly IPokemonService _pokemonService;

        public QuizFlow
            (IHttpContextAccessor httpContextAccessor,
            IRandomizer randomizer,
            IPokemonService pokemonService)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _randomizer = randomizer;
            _pokemonService = pokemonService;
        }
        public async Task<QuizViewModel> SetupQuiz(QuizDifficultyViewModel userEnteredQuestion, string pokemonName)
        {
            var totalCorrectAnswers = _session.GetInt32("amountCorrect").GetValueOrDefault();
            if (pokemonName == _session.GetString("pokemonAnswer") & pokemonName != null)
            {
                totalCorrectAnswers++;
                _session.SetInt32("amountCorrect", totalCorrectAnswers);
            }
            if (userEnteredQuestion.SelectedNumberOfQuestions != 0)
            {
                _session.SetInt32("questionsAttempted", userEnteredQuestion.SelectedNumberOfQuestions);
            }
            var quizModel = new QuizViewModel();

            userEnteredQuestion.SelectedNumberOfQuestions = userEnteredQuestion.SelectedNumberOfQuestions + 1;

            if (quizModel.PokemonAnswers.Count == 0)
            {

                quizModel.PokemonAnswers = _randomizer.RandomizeListOfAnsweres(userEnteredQuestion.SelectedNumberOfQuestions);
            }
            var testString = _session.GetString("pokemonStack");

            if (testString != null)
            {
                quizModel.PokemonAnswers = JsonConvert.DeserializeObject<Stack<int>>(_session.GetString("pokemonStack"));
            }

            quizModel.CorrectPokemon = await _pokemonService.MapPokemonInfo(quizModel.PokemonAnswers.Peek());
            var listOfWrongAnswers = _randomizer.RandomizeAditionalPokemon(quizModel.PokemonAnswers.Peek(), 4);
            quizModel.WrongAnswer1 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[0]);
            quizModel.WrongAnswer2 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[1]);
            quizModel.WrongAnswer3 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[2]);
            _session.SetString("pokemonAnswer", quizModel.CorrectPokemon.PokemonName);
            quizModel.PokemonAnswers.Pop();
            var storeStackIntoString = JsonConvert.SerializeObject(quizModel.PokemonAnswers);
            _session.SetString("pokemonStack", storeStackIntoString);
            quizModel.QuizAnswers.Add(quizModel.CorrectPokemon);
            quizModel.QuizAnswers.Add(quizModel.WrongAnswer1);
            quizModel.QuizAnswers.Add(quizModel.WrongAnswer2);
            quizModel.QuizAnswers.Add(quizModel.WrongAnswer3);
            quizModel.QuizAnswers = _randomizer.RandomizePossibleAnswerOrder(quizModel.QuizAnswers);

            return quizModel;
        }

        public int TotalQuetions => _session.GetInt32("questionsAttempted") ?? 0;
        public int QuestionsCorrect => _session.GetInt32("amountCorrect") ?? 0;
    }
}
