using Microsoft.AspNetCore.Mvc;
using ADHDAI.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenAI_API;
using Newtonsoft.Json.Linq;


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

        [HttpPost]
        public async Task<IActionResult> BrainDumpConfirmation(BrainDumpRequest request)
        {
            //    string jsonString = @"{
            //    'Objectives': [
            //        {
            //            'Type of Entry': 'To-Do',
            //            'Description': 'Pick up prescription',
            //            'Time': 'NA'
            //        },
            //        {
            //            'Type of Entry': 'Goal',
            //            'Description': 'Quit eating sugar',
            //            'Time': 'NA'
            //        },
            //        {
            //            'Type of Entry': 'Project',
            //            'Description': 'Remodel guest bathroom',
            //            'Time': 'NA'
            //        },
            //        {
            //            'Type of Entry': 'Calendar Event',
            //            'Description': 'Meet Kate for dinner',
            //            'Time': '18:00'
            //        },
            //        {
            //            'Type of Entry': 'Calendar Event',
            //            'Description': 'Appointment with Dr Beal',
            //            'Time': '11:30'
            //        }
            //    ]
            //}";

            string rawUserInput = request.input;

            string rawUserInputToJson = await OpenAI.DataParser(rawUserInput);

            List<Objective> userOutputList = Objective.GetObjectives(rawUserInputToJson);

            return View(userOutputList);



        }



        //[HttpPost]
        //public string ParseEventData([FromBody] BrainDumpRequest request)
        //{
        //    return "{}";
        //}

        public record BrainDumpRequest(string input);

        

    }
}
