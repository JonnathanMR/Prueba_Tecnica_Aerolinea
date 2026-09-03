using System;
using System.Collections.Generic;

namespace airport_services_api.Models;

public partial class Handler
{
    public int HandlerId { get; set; }

    public string HandlerName { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
