using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Objective
{
    [JsonProperty("Type of Entry")]
    public string TypeOfEntry { get; set; }
    public string Description { get; set; }
    public string Time { get; set; }

    public static List<Objective> GetObjectives(string jsonString)
    {
        var json = JsonConvert.DeserializeObject<dynamic>(jsonString);
        var objectives = JsonConvert.DeserializeObject<List<Objective>>(json.Objectives.ToString());

        return objectives;
    }
}


