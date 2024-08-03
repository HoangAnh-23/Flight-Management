using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Dao
{
    public class AirlineDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static AirlineDAO instance = null;
        private static readonly object instanceLock = new object();
        private AirlineDAO() { }
        public static AirlineDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AirlineDAO();
                    }
                    return instance;
                }
            }
        }

        //-------------------------------------
        public IEnumerable<Airline> GetAirlineList()
        {
            List<Airline> airlines;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                airlines = flightManagement.Airlines.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return airlines;
        }
        //-------------------------------------
        public Airline GetAirlineByID(int airlineID)
        {
            Airline airline = null;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                airline = flightManagement.Airlines.SingleOrDefault(a => a.Id == airlineID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return airline;
        }

        //-------------------------------------
        public void AddAirline(Airline airline)
        {
            try
            {
                Airline _airline = GetAirlineByID(airline.Id);
                if (_airline == null)
                {
                    var flightManagement = new FlightManagementDBContext();
                    flightManagement.Airlines.Add(airline);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The airline is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------
        public void Update(Airline airline)
        {
            try
            {
               
                Airline existingAirline = GetAirlineByID(airline.Id);
                if (existingAirline != null)
                {
                     var flightManagement = new FlightManagementDBContext();
                    flightManagement.Entry(existingAirline).CurrentValues.SetValues(airline);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The airline does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating airline: {ex.Message}");
            }
        }

        //-------------------------------------
        public void Remove(Airline airline)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Load the airline with related entities
                    var airlineToRemove = flightManagement.Airlines
                        .Include(a => a.Flights)
                            .ThenInclude(f => f.Bookings)
                                .ThenInclude(b => b.Baggages)
                        .FirstOrDefault(a => a.Id == airline.Id);

                    if (airlineToRemove != null)
                    {
                        // Remove related entities first
                        flightManagement.Baggages.RemoveRange(
                            airlineToRemove.Flights.SelectMany(f => f.Bookings).SelectMany(b => b.Baggages));
                        flightManagement.Bookings.RemoveRange(
                            airlineToRemove.Flights.SelectMany(f => f.Bookings));
                        flightManagement.Flights.RemoveRange(airlineToRemove.Flights);

                        // Remove the airline
                        flightManagement.Airlines.Remove(airlineToRemove);
                        flightManagement.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The airline does not already exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing airline: {ex.Message}");
            }
        }


        //----------------------------------------
        public IEnumerable<Airline> SearchByName(string search)
        {
            List<Airline> airlines;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                airlines = flightManagement.Airlines.Where(x => x.Name.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return airlines;
        }
        public int GetTotalCount()
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    return flightManagement.Airlines.Count(); // Đếm tổng số bản ghi
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total count of airlines: {ex.Message}");
            }
        }

        public IEnumerable<Airline> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Tính toán vị trí bắt đầu của trang hiện tại trong tập dữ liệu
                    int startIndex = (pageNumber - 1) * pageSize;

                    // Lấy dữ liệu cho trang hiện tại, với số lượng bản ghi là pageSize, bắt đầu từ vị trí startIndex
                    var airlines = flightManagement.Airlines
                        .OrderBy(p => p.Id) // Sắp xếp theo Id hoặc trường nào đó để đảm bảo thứ tự không thay đổi
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();

                    return airlines;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paged airlines: {ex.Message}");
            }
        }
    }
}
