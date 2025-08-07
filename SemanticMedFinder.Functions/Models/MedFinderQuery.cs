namespace SemanticMedFinder.Functions.Models
{
    internal class MedFinderQuery
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;


        public required string QueryPrompt { get; set; }
    }

    internal class CreateMedFinderQuery
    {
        public required string QueryPrompt { get; set; }
    }
}
