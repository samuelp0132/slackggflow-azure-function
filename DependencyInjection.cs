using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using slackggflow_azure_function.Services;

[assembly: FunctionsStartup(typeof(slackggflow_azure_function.DependencyInjection))]

namespace slackggflow_azure_function
{
    public class DependencyInjection : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IstackOverFlowService, StackOverFlowService>();
            builder.Services.AddTransient<ISlackService, slackService>();
        }
    }
}