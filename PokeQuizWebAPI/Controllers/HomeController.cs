using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeQuizWebAPI.Models;
using PokeQuizWebAPI.Models.PlayerModels;
using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PlayerServices;
using PokeQuizWebAPI.PokemonServices;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IQuizFlow _quizFlow;
        private readonly IPlayerService _playerService;

        public HomeController(IHttpContextAccessor httpContextAccessor,IQuizFlow quizFlow, IPlayerService playerService)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _quizFlow = quizFlow;
            _playerService = playerService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new QuizDifficultyViewModel();
            model.SelectedNumberOfQuestions = 2;
            string name = null;
            var models = await _quizFlow.SetupQuiz(model, name);
            var startModel = new PlayerRankModel();
            startModel = await _playerService.AssemblePlayerRank();
            return View(startModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
