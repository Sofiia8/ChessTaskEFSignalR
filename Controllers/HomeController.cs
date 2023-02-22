using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChessTaskEFSignalR.Models;
using ChessTaskEFSignalR.Services;

namespace ChessTaskEFSignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Step> _repository;
        private IPlay _play;

        public HomeController(IRepository<Step> repository, IPlay play)
        {
            _repository = repository;
            _play = play;
        }

        public IActionResult Index()
        {
            return View(_repository.GetListOfElems());
        }

        public IActionResult Start()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Start(int coordX, int coordY, double timeInterval)
        {
            _play.StartGame(coordX, coordY, timeInterval);
            ViewBag.coordX = _repository.GetFirstElemX();
            ViewBag.coordY = _repository.GetFirstElemY();
            return View("ChessGame");
        }
    }
}
