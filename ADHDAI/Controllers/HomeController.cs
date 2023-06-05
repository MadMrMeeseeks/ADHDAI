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
            return View(); ;
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
        public async Task<IActionResult> BrainDumpConfirmed(ParsedBrainDump parsedBrainDump)
        {
            // 1. Save to the calendar json file
            // 2. Tell user it worked

            return View();
        }

        [HttpPost] // (In this case***) Square brackets are attributes. Requires data be "posted". 
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

            return View(new ParsedBrainDump(userOutputList));
        }



        //[HttpPost]
        //public string ParseEventData([FromBody] BrainDumpRequest request)
        //{
        //    return "{}";
        //}

        public record BrainDumpRequest(string input);
        public record ParsedBrainDump(List<Objective> Objectives);
    }
}
