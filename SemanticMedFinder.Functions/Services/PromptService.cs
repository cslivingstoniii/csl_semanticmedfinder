using Microsoft.SemanticKernel;

namespace SemanticMedFinder.Functions.Services
{
    internal class PromptService
    {
        async public Task<string> InvokePromptAsync(Kernel kernel, string request)
        {
            string prompt = $"""
                            Instructions: What is the intent of this request?
                            If you don't know the intent, don't guess; instead respond with "Unknown".
                            Choices: SendEmail, SendMessage, CompleteTask, CreateDocument, Unknown.
                            User Input: {request}
                            Intent: 
                            """;

            return (await kernel.InvokePromptAsync($"{request}")).ToString();
        }
    }
}
