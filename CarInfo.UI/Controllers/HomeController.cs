using CarInfo.UI.Models;
using CarInfo.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarInfo.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpService _httpService;

        public HomeController(ILogger<HomeController> logger, HttpService httpService)
        {
            _logger = logger;
            _httpService = httpService;
        }

        public async Task<IActionResult> Index()
        {
            CarMakeResponse? result = await _httpService.GetAsync<CarMakeResponse>("getallmakes?format=json");

            if (result != null)
            {
                ViewBag.CarMakes = result.Results;
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleTypesForMakeId(int carMakeId)
        {
            if (carMakeId == 0)
            {
                return Json(new List<VehicleTypeDTO>());
            }

            VehicleTypeResponse? result = await _httpService.GetAsync<VehicleTypeResponse>($"GetVehicleTypesForMakeId/{carMakeId}?format=json");

            if (result != null)
            {
                return Json(result.Results);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetModelsForMakeIdYear(int carMakeId, int year)
        {
            if (carMakeId == 0 || year == 0)
            {
                return Json(new List<CarModelDTO>());
            }

            CarModelResponse? result = await _httpService.GetAsync<CarModelResponse>($"GetModelsForMakeIdYear/makeId/{carMakeId}/modelyear/{year}?format=json");

            if (result != null)
            {
                return Json(result.Results);
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
