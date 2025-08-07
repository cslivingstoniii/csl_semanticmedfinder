using Microsoft.SemanticKernel;
using SemanticMedFinder.Functions.Models;
using System.ComponentModel;

namespace SemanticMedFinder.Functions.Plugins
{
    public class TaskManagementPlugin
    {
        // Mock data for the tasks
        private readonly List<TaskModel> tasks = new()
        {
            new TaskModel { Id = 1, Title = "Design homepage", Description = "Create a modern homepage layout", Status = "In Progress", Priority = "High" },
            new TaskModel { Id = 2, Title = "Fix login bug", Description = "Resolve the issue with login sessions timing out", Status = "To Do", Priority = "Critical" },
            new TaskModel { Id = 3, Title = "Update documentation", Description = "Improve API reference for developers", Status = "Completed", Priority = "Medium" }
        };

        [KernelFunction("complete_task")]
        [Description("Updates the status of the specified task to Completed")]
        [return: Description("The updated task; will return null if the task does not exist")]
        public TaskModel? CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(task => task.Id == id);

            if (task == null)
            {
                return null;
            }

            task.Status = "Completed";

            return task;
        }

        [KernelFunction("get_critical_tasks")]
        [Description("Gets a list of all tasks marked as 'Critical' priority")]
        [return: Description("A list of critical tasks")]
        public List<TaskModel> GetCriticalTasks()
        {
            // Filter tasks with "Critical" priority
            return tasks.Where(task => task.Priority.Equals("Critical", StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
