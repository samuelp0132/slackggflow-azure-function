using System;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace slackggflow_azure_function.Services;

public interface IstackOverFlowService
{
    Task<string> MakeStackOverFlowGetRequest();
}

public class StackOverFlowService : IstackOverFlowService
{
    public async Task<string> MakeStackOverFlowGetRequest()
    {
        var epochTime = (Int32)(DateTime.UtcNow.AddDays(-1).Subtract(new DateTime(1970,1,1))).TotalSeconds;
        
        HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        using var client = new HttpClient(handler);
        var response = await client.GetAsync($"{Environment.GetEnvironmentVariable("StackOverFlowBase")}?fromdate={epochTime}&order=desc&sort=activity&intitle=.net&site=stackoverflow");

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}