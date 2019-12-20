using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeQuizWebAPI.PlayerServices;
using PokeQuizWebAPI.PokemonServices;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {

        private readonly ISession _session;
        private readonly IPokemonUserSQLService _pokemonUserSQLService;
        private readonly IPlayerService _playerService;

        public PlayerController(IHttpContextAccessor httpContextAccessor, IPokemonUserSQLService pokemonUserSQLService, IPlayerService playerService)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _pokemonUserSQLService = pokemonUserSQLService;
            _playerService = playerService;
        }


        public async Task<IActionResult> Rank()
        {
            var rankModel = await _playerService.AssemblePlayerRank();
            return View(rankModel);
        }
    }
}