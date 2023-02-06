using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace slackggflow_azure_function.Services;

public interface ISlackService
{
    Task<string> MakeSlackPostRequest(string message);
}

public class slackService : ISlackService
{
    public async Task<string> MakeSlackPostRequest(string message)
    {
        using var client = new HttpClient();
        var requestData = new StringContent("{'text':'" + message + "'}",
            Encoding.UTF8,
            "application/json");
        
        
        var response = await client.PostAsync(Environment.GetEnvironmentVariable("SlackHook"),
            requestData);

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}