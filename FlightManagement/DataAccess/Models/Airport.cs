using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightArrivingAirportNavigations = new HashSet<Flight>();
            FlightDepartingAirportNavigations = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Flight> FlightArrivingAirportNavigations { get; set; }
        public virtual ICollection<Flight> FlightDepartingAirportNavigations { get; set; }
    }
}
