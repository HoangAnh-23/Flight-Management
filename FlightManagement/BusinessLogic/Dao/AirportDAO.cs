using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dao
{
    public class AirportDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static AirportDAO instance = null;
        private static readonly object instanceLock = new object();
        private AirportDAO() { }
        public static AirportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AirportDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------
        public IEnumerable<Airport> GetAllList()
        {
            List<Airport> airports;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                airports = flightManagement.Airports.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return airports;
        }
        //-------------------------------------
        public Airport GetByID(int airportId)
        {
            Airport airport = null;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                airport = flightManagement.Airports.SingleOrDefault(a => a.Id == airportId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return airport;
        }

        //-------------------------------------
        public void Add(Airport airport)
        {
            try
            {
                Airport _airport = GetByID(airport.Id);
                if (_airport == null)
                {
                    var flightManagement = new FlightManagementDBContext();
                    flightManagement.Airports.Add(airport);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The airport is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------
        public void Update(Airport airport)
        {
            try
            {
                var flightManagement = new FlightManagementDBContext();
                Airport existing = flightManagement.Airports.FirstOrDefault(a => a.Id == airport.Id);
                if (existing != null)
                {
                    flightManagement.Entry(existing).CurrentValues.SetValues(airport);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The airport does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating airport: {ex.Message}");
            }
        }

        //-------------------------------------
        public void Remove(Airport airport)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    var airportToRemove = flightManagement.Airports
                        .Include(a => a.FlightDepartingAirportNavigations)
                            .ThenInclude(f => f.Bookings)
                                .ThenInclude(b => b.Baggages)
                        .Include(a => a.FlightArrivingAirportNavigations)
                            .ThenInclude(f => f.Bookings)
                                .ThenInclude(b => b.Baggages)
                        .FirstOrDefault(a => a.Id == airport.Id);

                    if (airportToRemove != null)
                    {
                        // Remove related entities first
                        // Departing flights
                        var departingFlights = airportToRemove.FlightDepartingAirportNavigations.ToList();
                        foreach (var flight in departingFlights)
                        {
                            flightManagement.Baggages.RemoveRange(flight.Bookings.SelectMany(b => b.Baggages));
                            flightManagement.Bookings.RemoveRange(flight.Bookings);
                        }
                        flightManagement.Flights.RemoveRange(departingFlights);

                        // Arriving flights
                        var arrivingFlights = airportToRemove.FlightArrivingAirportNavigations.ToList();
                        foreach (var flight in arrivingFlights)
                        {
                            flightManagement.Baggages.RemoveRange(flight.Bookings.SelectMany(b => b.Baggages));
                            flightManagement.Bookings.RemoveRange(flight.Bookings);
                        }
                        flightManagement.Flights.RemoveRange(arrivingFlights);

                        // Remove the airport
                        flightManagement.Airports.Remove(airportToRemove);
                        flightManagement.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The airport does not already exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing airport: {ex.Message}");
            }
        }
        //----------------------------------------
        public IEnumerable<Airport> SearchByName(string search)
        {
            List<Airport> airports;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                airports = flightManagement.Airports.Where(x => x.Name.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return airports;
        }
        public int GetTotalCount()
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    return flightManagement.Airports.Count(); // Đếm tổng số bản ghi
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total count of airports: {ex.Message}");
            }
        }

        public IEnumerable<Airport> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Tính toán vị trí bắt đầu của trang hiện tại trong tập dữ liệu
                    int startIndex = (pageNumber - 1) * pageSize;

                    // Lấy dữ liệu cho trang hiện tại, với số lượng bản ghi là pageSize, bắt đầu từ vị trí startIndex
                    var airports = flightManagement.Airports
                        .OrderBy(p => p.Id) // Sắp xếp theo Id hoặc trường nào đó để đảm bảo thứ tự không thay đổi
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();

                    return airports;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paged airlines: {ex.Message}");
            }
        }
    }
}
