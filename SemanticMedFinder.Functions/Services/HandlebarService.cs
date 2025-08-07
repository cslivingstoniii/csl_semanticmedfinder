using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

namespace SemanticMedFinder.Functions.Services
{
    internal class HandlebarService
    {
        async public Task<string> InvokePromptAsync(Kernel kernel, string request)
        {
            // build template
            const string HandlebarsTemplate = """
                                            <message role="system">You are an AI assistant designed to help with image recognition tasks.</message>
                                            <message role="user">
                                                <text>{{request}}</text>
                                                <image>{{imageData}}</image>
                                            </message>
                                            """;

            // Create the prompt template configuration
            var templateFactory = new HandlebarsPromptTemplateFactory();
            var promptTemplateConfig = new PromptTemplateConfig()
            {
                Template = HandlebarsTemplate,
                TemplateFormat = "handlebars",
                Name = "Vision_Chat_Prompt",
            };
            
            // Create a function from the Handlebars template configuration
            var function = kernel.CreateFunctionFromPrompt(promptTemplateConfig, templateFactory);

            var arguments = new KernelArguments(new Dictionary<string, object?>
            {
                {"request",request},
                {"imageData", "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAACVJREFUKFNj/KTO/J+BCMA4iBUyQX1A0I10VAizCj1oMdyISyEAFoQbHwTcuS8AAAAASUVORK5CYII="}
            });

            var response = await kernel.InvokeAsync(function, arguments);

            return response.ToString();
        }

        async public Task<string> InvokeTemplateAsync(Kernel kernel, string request)
        {
            string prompt = """
                            <message role="system">Instructions: Identify the from and to destinations 
                            and dates from the user's request</message>

                            <message role="user">Can you give me a list of flights from Seattle to Tokyo? 
                            I want to travel from March 11 to March 18.</message>

                            <message role="assistant">
                            Origin: Seattle
                            Destination: Tokyo
                            Depart: 03/11/2025 
                            Return: 03/18/2025
                            </message>

                            <message role="user">{{input}}</message>
                            """;

            string input = "I want to travel from June 1 to July 22. I want to go to Greece. I live in Chicago.";

            // Create the kernel arguments
            var arguments = new KernelArguments { ["input"] = input };

            // Create the prompt template config using handlebars format
            var templateFactory = new HandlebarsPromptTemplateFactory();
            var promptTemplateConfig = new PromptTemplateConfig()
            {
                Template = prompt,
                TemplateFormat = "handlebars",
                Name = "FlightPrompt",
            };

            // Invoke the prompt function
            var function = kernel.CreateFunctionFromPrompt(promptTemplateConfig, templateFactory);
            var response = await kernel.InvokeAsync(function, arguments);
            Console.WriteLine(response);

            return response.ToString();
        }
    }
}
