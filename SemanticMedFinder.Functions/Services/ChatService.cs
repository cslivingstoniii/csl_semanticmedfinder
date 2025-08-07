using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticMedFinder.Functions.Services
{
    internal class ChatService
    {
        private Kernel _kernel;
        private ChatHistory _chatHistory;
        private IChatCompletionService _chatCompletionService;

        public ChatService(Kernel kernel)
        {
            _kernel = kernel;
            _chatHistory = [];
            _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
        }

        async public Task<string> InvokePromptAsync(Kernel kernel, string request)
        {
            // Create a chat history object
            ChatHistory chatHistory = [];

            // Add role messages to the chat history
            chatHistory.AddSystemMessage("You are a helpful assistant.");
            chatHistory.AddUserMessage("What's available to order?");
            chatHistory.AddAssistantMessage("We have pizza, pasta, and salad available to order. What would you like to order?");
            chatHistory.AddUserMessage("I'd like to have the first option, please.");

            for (int i = 0; i < chatHistory.Count; i++)
            {
                Console.WriteLine($"{chatHistory[i].Role}: {chatHistory[i]}");
            }

            // Add user message with an image
#pragma warning disable SKEXP0001 // AuthorName is subject to change and emits a warning
            chatHistory.Add(
                new()
                {
                    Role = AuthorRole.User,
                    AuthorName = "Laimonis Dumins",
                    Items = [
                        new TextContent { Text = "What available on this menu" },
            new ImageContent { Uri = new Uri("https://example.com/menu.jpg") }
                    ]
                }
            );

            var result = string.Empty;
            return result.ToString();
        }

        async public Task<string> InvokeChatAsync(string request)
        {
            // Prompt the LLM
            _chatHistory.AddSystemMessage("You are a helpful travel assistant.");
            _chatHistory.AddSystemMessage("Recommend a destination to the traveler based on their background and preferences.");

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

            for (int i = 0; i < _chatHistory.Count; i++)
            {
                Console.WriteLine($"{_chatHistory[i].Role}: {_chatHistory[i]}");
            }

            var result = string.Empty;
            return result.ToString();
        }

        void AddMessage(string msg)
        {
            Console.WriteLine(msg);
            _chatHistory.AddAssistantMessage(msg);
        }

        void GetInput()
        {
            string input = Console.ReadLine()!;
            _chatHistory.AddUserMessage(input);
        }

        async Task GetReply()
        {
            ChatMessageContent reply = await _chatCompletionService.GetChatMessageContentAsync(
                _chatHistory,
                kernel: _kernel
            );
            Console.WriteLine(reply.ToString());
            _chatHistory.AddAssistantMessage(reply.ToString());
        }
    }
}
