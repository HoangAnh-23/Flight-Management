using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BusinessLogic.Dao
{
    public class BaggageDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static BaggageDAO instance = null;
        private static readonly object instanceLock = new object();
        private BaggageDAO() { }
        public static BaggageDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BaggageDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------
        public IEnumerable<Baggage> GetAllList()
        {
            List<Baggage> baggage;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                baggage = flightManagement.Baggages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return baggage;
        }
        //-------------------------------------
        public Baggage GetByID(int baggageId)
        {
            Baggage baggage = null;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                baggage = flightManagement.Baggages.SingleOrDefault(a => a.Id == baggageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return baggage;
        }

        //-------------------------------------
        public void Add(Baggage baggage)
        {
            try
            {
                Baggage _baggage = GetByID(baggage.Id);
                if (_baggage == null)
                {
                    var flightManagement = new FlightManagementDBContext();
                    flightManagement.Baggages.Add(baggage);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The Baggage is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------
        public void Update(Baggage baggage)
        {
            try
            {
                var flightManagement = new FlightManagementDBContext();
                Baggage existing = flightManagement.Baggages.FirstOrDefault(a => a.Id == baggage.Id);
                if (existing != null)
                {
                    flightManagement.Entry(existing).CurrentValues.SetValues(baggage);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The baggage does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating baggage: {ex.Message}");
            }
        }

        //-------------------------------------
        public void Remove(Baggage baggage)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Load the bookingPlatform with related entities
                    var toRemove = flightManagement.Baggages
                        .Include(a => a.Booking)
                        .FirstOrDefault(a => a.Id == baggage.Id);

                    if (toRemove != null)
                    {
                        // Remove related entities first
                     
                        flightManagement.Bookings.RemoveRange(toRemove.Booking);

                    
                        flightManagement.Baggages.Remove(toRemove);
                        flightManagement.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The baggage does not already exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing baggage: {ex.Message}");
            }
        }
        //----------------------------------------
        public IEnumerable<Baggage> SearchByWeight(decimal weight)
        {
            List<Baggage> baggages;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                baggages = flightManagement.Baggages.Where(x => x.WeightInKg == weight).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return baggages;
        }
        public int GetTotalCount()
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    return flightManagement.Baggages.Count(); // Đếm tổng số bản ghi
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total count of baggages: {ex.Message}");
            }
        }

        public IEnumerable<Baggage> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Tính toán vị trí bắt đầu của trang hiện tại trong tập dữ liệu
                    int startIndex = (pageNumber - 1) * pageSize;

                    // Lấy dữ liệu cho trang hiện tại, với số lượng bản ghi là pageSize, bắt đầu từ vị trí startIndex
                    var baggages = flightManagement.Baggages
                        .OrderBy(p => p.Id) // Sắp xếp theo Id hoặc trường nào đó để đảm bảo thứ tự không thay đổi
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();

                    return baggages;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paged baggages: {ex.Message}");
            }
        }
    }
}
