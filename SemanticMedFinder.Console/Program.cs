using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticMedFinder.Plugins;

// Populate values from your OpenAI deployment
var modelId = "gpt-4o-mini";
var endpoint = "https://api.openai.com/v1/chat/completions";
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("Missing API key");

// Create a kernel with Azure OpenAI chat completion then build the kernel
var builder = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(modelId, apiKey);
builder.Plugins.AddFromType<TravelTipsPlugin>();
Kernel kernel = builder.Build();

// invoke the TravelTipsPlugin
var result = await kernel.InvokeAsync("TravelTipsPlugin", "GetTravelPhrase", new() { ["phrase"] = "hello" });
Console.WriteLine(result);



// Get chat completion service.
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Create a chat history object
ChatHistory chatHistory = [];

void AddMessage(string msg)
{
    Console.WriteLine(msg);
    chatHistory.AddAssistantMessage(msg);
}

void GetInput()
{
    string input = Console.ReadLine()!;
    chatHistory.AddUserMessage(input);
}

async Task GetReply()
{
    ChatMessageContent reply = await chatCompletionService.GetChatMessageContentAsync(
        chatHistory,
        kernel: kernel
    );
    Console.WriteLine(reply.ToString());
    chatHistory.AddAssistantMessage(reply.ToString());
}

// Prompt the LLM
chatHistory.AddSystemMessage("You are a helpful travel assistant.");
chatHistory.AddSystemMessage("Recommend a destination to the traveler based on their background and preferences.");

// Get information about the user's plans
AddMessage("Tell me about your travel plans.");
GetInput();
await GetReply();

// Offer recommendations
AddMessage("Would you like some activity recommendations?");
GetInput();
await GetReply();

// Offer language tips
AddMessage("Would you like some helpful phrases in the local language?");
GetInput();
await GetReply();

Console.WriteLine("Chat Ended.\n");
Console.WriteLine("Chat History:");

for (int i = 0; i < chatHistory.Count; i++)
{
    Console.WriteLine($"{chatHistory[i].Role}: {chatHistory[i]}");
}