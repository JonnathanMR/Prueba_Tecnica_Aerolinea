namespace airport_services_api.Dto
{
    public class CreateFlightRequest
    {
        public string FlightNumber { get; set; } = null!;
        public DateTime DepartureTime { get; set; }
        public string ServiceType { get; set; } = null!;
        public int DurationMinutes { get; set; }
        public int AirlineId { get; set; }
        public int HandlerId { get; set; }
    }
}
