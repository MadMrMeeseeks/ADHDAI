using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class OpenAI
{
    public static string openAItestAPI = "";



    static async Task Main()
    {
        Console.Write("Enter input: ");
        string userInput = Console.ReadLine();

        string formattedUserData = await DataParser(userInput);

        Console.WriteLine(formattedUserData);



    }



    static public async Task<string> DataParser(string rawUserInput)
    {

        string openAIKey = "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";
        //string openAIKey = "";

        string url = "https://api.openai.com/v1/engines/text-davinci-003/completions";

        string aiGuidance = "Take unstructured user entries. Break the information down into the following categories: To-Do, Goal. Project and Calendar Event. To-Dos are one off tasks which may be recurring and can be due on any day for example: “Pick up prescription” would be a to-do. Goals are something the user wants to accomplish,for example “I am having trouble quitting sugar and need help” The goal would be to quit eating sugar. Projects are things the user wants to build or create, for example “I want to renovate my backyard” would be an example of a project. Finally, a Calendar Event is something that needs to be added to the user's calendar, such as dinner plans, meetings and other common calendar worthy events. For the TIME column, Calendar events should be formatted in a 24 time format (for example 6:00pm should be 18:00) and are required to have a time. The other events can be NA, TODAY, or and actual time depending on the case. Please convert the table to the corresponding JSON format ";

        string jsonExample = """
            {
              "Objectives": [
                {
                  "Type of Entry": "",
                  "Description": "",
                  "Time": ""
                }
              ]
            }
            """;

        var prompt = $"{aiGuidance}\n\n{rawUserInput}\n\n{jsonExample}";




        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAIKey}");

            var requestData = new
            {
                prompt = prompt,
                temperature = 0,
                max_tokens = 1000,
                top_p = 1,
                frequency_penalty = 0,
                presence_penalty = 0
            };

            var jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(url, content))
            {
                response.EnsureSuccessStatusCode();

                var jsonResponseData = await response.Content.ReadAsStringAsync();
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponseData);
                string completionText = responseObject.choices[0].text;

                return (completionText);
            }
        }
    }
}
