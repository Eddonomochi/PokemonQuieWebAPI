using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeQuizWebAPI.Models;
using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonServices;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IQuizFlow _quizFlow;

        public HomeController(IHttpContextAccessor httpContextAccessor,IQuizFlow quizFlow)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _quizFlow = quizFlow;
        }
        public async Task<IActionResult> Index()
        {
            
            var model = new QuizDifficultyViewModel();
            model.SelectedNumberOfQuestions = 2;
            string name = null;
            var models = await _quizFlow.SetupQuiz(model, name);
            return View();
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
