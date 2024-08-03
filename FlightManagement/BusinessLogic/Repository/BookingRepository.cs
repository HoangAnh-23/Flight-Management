using DataAccess.Models;
using BusinessLogic.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public void delete(Booking booking)=>BookingDAO.Instance.Remove(booking);

        public IEnumerable<Booking> GetAll() => BookingDAO.Instance.GetAllList();
        public Booking GetByID(int Id) => BookingDAO.Instance.GetByID(Id);

        public IEnumerable<Booking> GetPaged(int pageNumber, int pageSize)=> BookingDAO.Instance.GetPaged(pageNumber, pageSize);    

        public int GetTotalCount()=>BookingDAO.Instance.GetTotalCount();

        public void insert(Booking booking) => BookingDAO.Instance.Add(booking);

        public IEnumerable<Booking> SearchByTime(DateTime searchTime) => BookingDAO.Instance.SearchByTime(searchTime);

        public void update(Booking booking) => BookingDAO.Instance.Update(booking);
    }
}
