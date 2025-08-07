using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SemanticMedFinder.Functions.Services;
using System.Text.Json;

namespace SemanticMedFinder.Functions
{
    public class MedFinderApi
    {
        private readonly ILogger<MedFinderApi> _logger;

        public MedFinderApi(ILogger<MedFinderApi> logger)
        {
            _logger = logger;
        }

        [Function("GetProvider")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "provider")] HttpRequest req)
        {
            _logger.LogInformation("/provider called");


            // Build the Request
            // Read the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize JSON to extract the "request" string
            var jsonDocument = JsonDocument.Parse(requestBody);
            string request = jsonDocument.RootElement.GetProperty("request").GetString();

            // Populate values from your OpenAI deployment
            var modelId = "gpt-4o-mini";
            var endpoint = "https://api.openai.com/v1/chat/completions";
            var apiKey = "";

            // Create a kernel with Azure OpenAI chat completion then build the kernel
            var builder = Kernel.CreateBuilder().AddOpenAIChatCompletion(modelId, apiKey);
            Kernel kernel = builder.Build();

            // promptservice
            //var promptService =  new PromptService();
            //var result = await promptService.InvokePromptAsync(kernel, request);

            // functionservice
            //var functionService = new FunctionService();
            //var result = await functionService.InvokePromptAsync(kernel, request);

            // handlebarservice
            //var handlebarService = new HandlebarService();
            //var result = await handlebarService.InvokeTemplateAsync(kernel, request);

            // chatservice
            //var chatService = new ChatService(kernel);
            //var result = await chatService.InvokeChatAsync(request);

            // chatservice
            var pluginService = new PluginService();
            var result = await pluginService.InvokePluginAsync(kernel, request);

            return new OkObjectResult(result.ToString());
        }
    }
}
