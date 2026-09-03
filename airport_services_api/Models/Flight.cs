using System;
using System.Collections.Generic;

namespace airport_services_api.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public string ServiceType { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public DateTime CreatedAt { get; set; }

    public int AirlineId { get; set; }

    public int HandlerId { get; set; }

    public virtual Airline Airline { get; set; } = null!;

    public virtual Handler Handler { get; set; } = null!;
}
