using Newtonsoft.Json.Linq;
using System.IO;

public static class Config
{
    public static string GetBaseUrl()
    {
        var json = File.ReadAllText("appsettings.json");
        var config = JObject.Parse(json);
        return config["TestSettings"]["BaseUrl"].ToString();
    }
}
