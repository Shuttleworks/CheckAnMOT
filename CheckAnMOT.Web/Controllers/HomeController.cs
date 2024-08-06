using CheckAnMOT.Core.Helpers;
using CheckAnMOT.Core.Models;
using CheckAnMOT.Core.Services;
using CheckAnMOT.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CheckAnMOT.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMotService _motService;

        public HomeController(ILogger<HomeController> logger, IMotService motService)
        {
            _logger = logger;
            _motService = motService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckMOT(string numberplate)
        {
            if (!string.IsNullOrEmpty(numberplate))
            {
                numberplate = Helpers.RemoveUserWhiteSpace(numberplate);

                if (Helpers.ValidRegPlate(numberplate))
                {
                    ResultDTO result = await _motService.GetVehicleMot(numberplate);
                    return View("Index", result);
                } else
                {
                    ResultDTO resultDTO = new() { ErrorMessage = "Invalid numberplate entered!" };
                    return View("Index", resultDTO);
                }

            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
