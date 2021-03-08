using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RomanHelper.Extension;
using RomanNumbersApp.Models;

namespace RomanNumbersApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RomanToInteger([FromBody] RomanNumber romanNumber)
        {
            //int number = int.TryParse(romanNumber.Number, out int result) ? result : 0;
            int number = romanNumber.Number.ConvertToInteger();
            return Ok(number);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
