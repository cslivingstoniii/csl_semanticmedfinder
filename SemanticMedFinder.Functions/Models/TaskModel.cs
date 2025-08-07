namespace SemanticMedFinder.Functions.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }

        // Constructor (optional)
        public TaskModel(int id, string title, string description, string status, string priority)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
        }

        // Default constructor (if needed for serialization/deserialization)
        public TaskModel() { }
    }

}
