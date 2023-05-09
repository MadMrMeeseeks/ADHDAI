using Microsoft.AspNetCore.Mvc;
using ADHDAI.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenAI_API;


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

        public IActionResult BrainDump()
        {
            return View();
        }


    }
}
