using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking GetByID(int Id);
        void insert(Booking booking);
        void update(Booking booking);
        void delete(Booking booking);
        IEnumerable<Booking> SearchByTime(DateTime searchTime);
        int GetTotalCount();
        IEnumerable<Booking> GetPaged(int pageNumber, int pageSize);
    }
}
