using Microsoft.SemanticKernel;
using SemanticMedFinder.Functions.Plugins;

namespace SemanticMedFinder.Functions.Services
{
    internal class PluginService
    {
        async public Task<string> InvokePluginAsync(Kernel kernel, string request)
        {
            // Add the plugin to the kernel
            kernel.Plugins.AddFromType<TaskManagementPlugin>("TaskManagement");

            // Invoke the function
            var arguments = new KernelArguments { ["id"] = int.Parse(request) };
            var updatedTask = await kernel.InvokeAsync("TaskManagement", "complete_task", arguments);

            var result = updatedTask.ToString();
            return result.ToString();
        }
    }
}
