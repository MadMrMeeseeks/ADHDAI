using Microsoft.AspNetCore.Mvc;
using ADHDAI.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ADHDAI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Snapshot"); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Snapshot()
        {
            return View();
        }

        public IActionResult Todos()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCalendarData([FromBody] Dictionary<string, string> calendarData)
        {
            System.IO.File.WriteAllText("calendarData.json", JsonConvert.SerializeObject(calendarData));
            return Ok();
        }

        [HttpGet]
        public IActionResult GetCalendarData()
        {
            if (System.IO.File.Exists("calendarData.json"))
            {
                var content = System.IO.File.ReadAllText("calendarData.json");
                var calendarData = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                return Json(calendarData);
            }
            else
            {
                return Json(new Dictionary<string, string>());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
