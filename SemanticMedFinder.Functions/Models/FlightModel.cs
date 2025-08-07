namespace SemanticMedFinder.Functions.Models
{
    public class FlightModel
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }

        // Constructor (optional)
        public FlightModel(int id, string airline, string destination, DateTime departureDate, decimal price, bool isBooked)
        {
            Id = id;
            Airline = airline;
            Destination = destination;
            DepartureDate = departureDate;
            Price = price;
            IsBooked = isBooked;
        }

        // Default constructor (if needed for serialization/deserialization)
        public FlightModel() { }
    }
}
