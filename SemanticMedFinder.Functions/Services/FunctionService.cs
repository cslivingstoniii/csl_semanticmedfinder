using Microsoft.SemanticKernel;

namespace SemanticMedFinder.Functions.Services
{
    internal class FunctionService
    {
        async public Task<string> InvokePromptAsync(Kernel kernel, string request)
        {
            // build prompt
            string[] parts = request.Split(',', 2); // Split into two parts only
            string city = parts[0];
            string background = parts.Length > 1 ? parts[1] : string.Empty;
            string prompt = """
                            You are a helpful travel guide. 
                            I'm visiting {{$city}}. {{$background}}. What are some activities I should do today?
                            """;

            // create the kernel function and arguments
            var activitiesFunction = kernel.CreateFunctionFromPrompt(prompt);
            var arguments = new KernelArguments { ["city"] = city, ["background"] = background };

            // InvokeAsync on the kernel object
            var result = await kernel.InvokeAsync(activitiesFunction, arguments);
            Console.WriteLine(result);

            return result.ToString();
        }
    }
}
