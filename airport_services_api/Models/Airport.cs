using System;
using System.Collections.Generic;

namespace airport_services_api.Models;

public partial class Airport
{
    public int AirportId { get; set; }

    public string IataCode { get; set; } = null!;

    public string AirportName { get; set; } = null!;

    public virtual ICollection<Airline> Airlines { get; set; } = new List<Airline>();
}
