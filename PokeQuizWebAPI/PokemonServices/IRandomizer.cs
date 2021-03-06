﻿using PokeQuizWebAPI.Models.PokemonViewModels;
using System.Collections.Generic;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IRandomizer
    {
        List<int> RandomizeAditionalPokemon(int answer, int amountOfPossibleAnswers);
        Stack<int> RandomizeListOfAnsweres(int quizLength);
        List<PokemonResponse> RandomizePossibleAnswerOrder(List<PokemonResponse> pokeAnswers);
    }
}
