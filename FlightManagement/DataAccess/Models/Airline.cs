using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Airline
    {
        public Airline()
        {
            Flights = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
