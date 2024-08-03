using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
