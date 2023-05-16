using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class OpenAIRawDataParser
{
    public static string openAItestAPI = "";



    static async Task Main()
    {
        Console.Write("Enter input: ");
        string userInput = Console.ReadLine();

        string formattedUserData = await DataParser(userInput);

        Console.WriteLine(formattedUserData);



    }

    static async Task<string> DataParser(string rawUserInput)
    {

        string openAIKey = openAItestAPI;

        string url = "https://api.openai.com/v1/engines/text-davinci-003/completions";

        string aiGuidance = File.ReadAllText(@"C:\src\OpenAITesting\OpenAITesting\Prompt.txt");

        string jsonExample = """
            | Type of Entry | Description | Time |

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
