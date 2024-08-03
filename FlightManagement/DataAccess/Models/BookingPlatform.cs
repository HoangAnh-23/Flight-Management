using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BookingPlatform
    {
        public BookingPlatform()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
