using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Dao
{
    public class FlightDAO
    {
        //-------------------------------------
        // Using Singleton Pattern
        private static FlightDAO instance = null;
        private static readonly object instanceLock = new object();
        private FlightDAO() { }
        public static FlightDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FlightDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------
        public IEnumerable<Flight> GetAllList()
        {
            List<Flight> flights;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                flights = flightManagement.Flights.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flights;
        }
        //-------------------------------------
        public Flight GetByID(int flightId)
        {
            Flight flight = null;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                flight = flightManagement.Flights.SingleOrDefault(f => f.Id == flightId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flight;
        }

        //-------------------------------------
        public void Add(Flight flight)
        {
            try
            {
                Flight _flight = GetByID(flight.Id);
                if (_flight == null)
                {
                    var flightManagement = new FlightManagementDBContext();
                    flightManagement.Flights.Add(flight);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The flight already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------
        public void Update(Flight flight)
        {
            try
            {
                var flightManagement = new FlightManagementDBContext();
                Flight existing = flightManagement.Flights.FirstOrDefault(f => f.Id == flight.Id);
                if (existing != null)
                {
                    flightManagement.Entry(existing).CurrentValues.SetValues(flight);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The flight does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating flight: {ex.Message}");
            }
        }

        //-------------------------------------
        public void Remove(Flight flight)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    var flightToRemove = flightManagement.Flights
                        .Include(f => f.Bookings)
                            .ThenInclude(b => b.Baggages)
                        .FirstOrDefault(f => f.Id == flight.Id);

                    if (flightToRemove != null)
                    {
                        // Remove related entities first
                        flightManagement.Baggages.RemoveRange(flightToRemove.Bookings.SelectMany(b => b.Baggages));
                        flightManagement.Bookings.RemoveRange(flightToRemove.Bookings);

                        // Remove the flight
                        flightManagement.Flights.Remove(flightToRemove);
                        flightManagement.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The flight does not already exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing flight: {ex.Message}");
            }
        }
        //----------------------------------------
        public IEnumerable<Flight> SearchByName(string search)
        {
            List<Flight> flights;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                flights = flightManagement.Flights.Where(x => x.ArrivingGate.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flights;
        }
        public int GetTotalCount()
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    return flightManagement.Flights.Count(); // Đếm tổng số bản ghi
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total count of flights: {ex.Message}");
            }
        }

        public IEnumerable<Flight> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Tính toán vị trí bắt đầu của trang hiện tại trong tập dữ liệu
                    int startIndex = (pageNumber - 1) * pageSize;

                    // Lấy dữ liệu cho trang hiện tại, với số lượng bản ghi là pageSize, bắt đầu từ vị trí startIndex
                    var flights = flightManagement.Flights
                        .OrderBy(p => p.Id) // Sắp xếp theo Id hoặc trường nào đó để đảm bảo thứ tự không thay đổi
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();

                    return flights;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paged flights: {ex.Message}");
            }
        }
        public IEnumerable<FlightStatistics> GetStatistics()
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    var monthlyStatistics = flightManagement.Flights
                        .GroupBy(f => new { f.DepartureTime.Value.Year, f.DepartureTime.Value.Month })
                        .Select(g => new FlightStatistics
                        {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            FlightCount = g.Count()
                        })
                        .OrderBy(s => s.Year).ThenBy(s => s.Month)
                        .ToList();

                    return monthlyStatistics;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting flight statistics: {ex.Message}");
            }
        }
    }
}
