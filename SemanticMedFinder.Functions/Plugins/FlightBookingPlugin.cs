using Microsoft.SemanticKernel;
using SemanticMedFinder.Functions.Models;
using System.ComponentModel;
using StackExchange.Redis;
using System.Text.Json;

namespace SemanticMedFinder.Functions.Plugins
{
    public class FlightBookingPlugin
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public FlightBookingPlugin()
        {
            _redis = ConnectionMultiplexer.Connect("localhost:6379");
            _db = _redis.GetDatabase();
        }

        // Mock data for the flights
        private readonly List<FlightModel> flights = new()
        {
            new FlightModel { Id = 101, Airline = "Skyward Express", Destination = "Tokyo", DepartureDate = new DateTime(2025, 5, 15), Price = 1250.75m, IsBooked = true },
            new FlightModel { Id = 102, Airline = "Delta", Destination = "New York", DepartureDate = new DateTime(2025, 4, 11), Price = 1175.50m, IsBooked = false },
            new FlightModel { Id = 103, Airline = "American Airlines", Destination = "Paris", DepartureDate = new DateTime(2025, 3, 22), Price = 999.22m, IsBooked = true }
        };

        [KernelFunction("search_flights")]
        [Description("Searches for available flights based on the destination and departure date in the format YYYY-MM-DD")]
        [return: Description("A list of available flights")]
        public List<FlightModel> SearchFlights(string destination, string departureDate)
        {
            // Filter flights based on destination
            return flights.Where(flight =>
                flight.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                flight.DepartureDate.Equals(departureDate)).ToList();
        }

        [KernelFunction("book_flight")]
        [Description("Books a flight based on the flight ID provided")]
        [return: Description("Booking confirmation message")]
        public async Task<string> BookFlight(int flightId)
        {
            var flight = flights.FirstOrDefault(f => f.Id == flightId);
            if (flight == null)
            {
                return "Flight not found. Please provide a valid flight ID.";
            }

            if (flight.IsBooked)
            {
                return $"You've already booked this flight.";
            }

            flight.IsBooked = true;
            await SaveFlightToRedis(flight);

            return $"Flight booked successfully! Airline: {flight.Airline}, Destination: {flight.Destination}, Departure: {flight.DepartureDate}, Price: ${flight.Price}.";
        }

        [KernelFunction("get_flight")]
        [Description("Gets a flight based on the flight ID provided")]
        [return: Description("Flight based on provided id, null if not found")]
        public async Task<string> GetFlight(int flightId)
        {
            var flight = await GetFlightFromRedis(flightId);
            if (flight == null)
            {
                return "Flight not found. Please provide a valid flight ID.";
            }

            if (flight.IsBooked)
            {
                return $"You've already booked this flight.";
            }

            flight.IsBooked = true;
            await SaveFlightToRedis(flight);

            return $"Flight booked successfully! Airline: {flight.Airline}, Destination: {flight.Destination}, Departure: {flight.DepartureDate}, Price: ${flight.Price}.";
        }

        private async Task SaveFlightToRedis(FlightModel flight)
        {
            await _db.StringSetAsync(flight.Id.ToString(), JsonSerializer.Serialize(flight));           
        }

        private async Task<FlightModel> GetFlightFromRedis(int id)
        {
            string flightJson = await _db.StringGetAsync(id.ToString());
            return flightJson is not null ? JsonSerializer.Deserialize<FlightModel>(flightJson) : null;
        }
    }
}
