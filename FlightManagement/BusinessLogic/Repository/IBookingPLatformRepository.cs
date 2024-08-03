using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IBookingPLatformRepository
    {
        IEnumerable<BookingPlatform> GetAll();
        BookingPlatform GetBookingPlatformByID(int Id);
        void insert(BookingPlatform bookingPlatform);
        void update(BookingPlatform bookingPlatform);
        void delete(BookingPlatform bookingPlatform);
        int GetTotalCount();
        IEnumerable<BookingPlatform> GetPaged(int pageNumber, int pageSize);
    /*    IEnumerable<BookingPlatform> SearchByName(string search);*/
    }
}
