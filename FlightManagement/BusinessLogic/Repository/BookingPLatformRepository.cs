using DataAccess.Models;
using BusinessLogic.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class BookingPLatformRepository : IBookingPLatformRepository
    {
        public void delete(BookingPlatform bookingPlatform)=>BookingPlatformDAO.Instance.Remove(bookingPlatform);

        public IEnumerable<BookingPlatform> GetAll()=>BookingPlatformDAO.Instance.GetBookingPlatformList();

        public BookingPlatform GetBookingPlatformByID(int Id)=>BookingPlatformDAO.Instance.GetBookingPlatformByID(Id);

        public IEnumerable<BookingPlatform> GetPaged(int pageNumber, int pageSize)=> BookingPlatformDAO.Instance.GetPaged(pageNumber, pageSize);
        public int GetTotalCount()=>BookingPlatformDAO.Instance.GetTotalCount();
        public void insert(BookingPlatform bookingPlatform)=>BookingPlatformDAO.Instance.Add(bookingPlatform);

        public void update(BookingPlatform bookingPlatform)=>BookingPlatformDAO.Instance.Update(bookingPlatform);
    }
}
