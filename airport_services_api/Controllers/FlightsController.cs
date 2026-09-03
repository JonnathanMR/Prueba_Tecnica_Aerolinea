using airport_services_api.Context;
using airport_services_api.Dto;

using airport_services_api.Models;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace airport_services_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly AirlineServicesContext? _airlineServicesContext;

        [HttpGet]
        public async Task<IActionResult> GetAllFlights() {
            var flights = await _airlineServicesContext.Flights
                .AsNoTracking()
                .Select(flight => new
            {
                flight.FlightId,
                flight.FlightNumber,
                flight.DepartureTime,
                flight.ServiceType,
                flight.DurationMinutes,

                Airline = flight.Airline.AirlineName,
                Airport = flight.Airline.Airport.AirportName,
                IataCode = flight.Airline.Airport.IataCode,
                Handler = flight.Handler.HandlerName
            })
            .ToListAsync();

            return Ok(flights);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            var flight = await _airlineServicesContext.Flights.FindAsync(id);

            return flight is null ? NotFound() : Ok(flight);
        }

        [HttpPost]
        public async Task <IActionResult> CreateFlight(CreateFlightRequest flightsRequest) {
            var flight = new Flight
            {
                FlightNumber = flightsRequest.FlightNumber,
                DepartureTime = flightsRequest.DepartureTime,
                ServiceType = flightsRequest.ServiceType,
                DurationMinutes = flightsRequest.DurationMinutes,
                CreatedAt = DateTime.UtcNow,
                AirlineId = flightsRequest.AirlineId,
                HandlerId = flightsRequest.HandlerId
            };

            _airlineServicesContext.Flights.Add(flight);
            await _airlineServicesContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetFlight),
                new { id = flight.FlightId },
                flight
            );
        }
    }
}
