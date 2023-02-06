using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using slackggflow_azure_function.Services;

namespace slackggflow_azure_function
{
    public class slack_flow
    {
        private readonly IstackOverFlowService _stackOverFlowService;
        private readonly ISlackService _slackService;
        
        public slack_flow(IstackOverFlowService stackOverFlowService, ISlackService slackService)
        {
            _stackOverFlowService = stackOverFlowService;
            _slackService = slackService;
        }
        [FunctionName("slack_flow")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var jsonString = await _stackOverFlowService.MakeStackOverFlowGetRequest();
            
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
            
            var newQuestionCount = jsonObject.items.Count;
            
            await _slackService.MakeSlackPostRequest($"You have {newQuestionCount} questions on Stack Overflow");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
