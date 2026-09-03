using System;
using System.Collections.Generic;

namespace airport_services_api.Models;

public partial class Airline
{
    public int AirlineId { get; set; }

    public string AirlineName { get; set; } = null!;

    public int AirportId { get; set; }

    public virtual Airport Airport { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
