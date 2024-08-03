using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Dao
{
    public class PassengerDAO
    {
        //-------------------------------------
        // Using Singleton Pattern
        private static PassengerDAO instance = null;
        private static readonly object instanceLock = new object();
        private PassengerDAO() { }
        public static PassengerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PassengerDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------
        public IEnumerable<Passenger> GetAllList()
        {
            List<Passenger> passengers;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                passengers = flightManagement.Passengers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return passengers;
        }
        //-------------------------------------
        public Passenger GetByID(int passengerId)
        {
            Passenger passenger = null;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                passenger = flightManagement.Passengers.SingleOrDefault(p => p.Id == passengerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return passenger;
        }

        //-------------------------------------
        public void Add(Passenger passenger)
        {
            try
            {
                Passenger _passenger = GetByID(passenger.Id);
                if (_passenger == null)
                {
                    var flightManagement = new FlightManagementDBContext();
                    flightManagement.Passengers.Add(passenger);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The passenger is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------
        public void Update(Passenger passenger)
        {
            try
            {
                var flightManagement = new FlightManagementDBContext();
                Passenger existing = flightManagement.Passengers.FirstOrDefault(p => p.Id == passenger.Id);
                if (existing != null)
                {
                    flightManagement.Entry(existing).CurrentValues.SetValues(passenger);
                    flightManagement.SaveChanges();
                }
                else
                {
                    throw new Exception("The passenger does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating passenger: {ex.Message}");
            }
        }

        //-------------------------------------
        public void Remove(Passenger passenger)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    var passengerToRemove = flightManagement.Passengers
                        .Include(p => p.Bookings)
                            .ThenInclude(b => b.Baggages)
                        .FirstOrDefault(p => p.Id == passenger.Id);

                    if (passengerToRemove != null)
                    {
                        // Remove related entities first
                        flightManagement.Baggages.RemoveRange(passengerToRemove.Bookings.SelectMany(b => b.Baggages));
                        flightManagement.Bookings.RemoveRange(passengerToRemove.Bookings);

                        // Remove the passenger
                        flightManagement.Passengers.Remove(passengerToRemove);
                        flightManagement.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The passenger does not already exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing passenger: {ex.Message}");
            }
        }
        //----------------------------------------
        public IEnumerable<Passenger> SearchByName(string search)
        {
            List<Passenger> passengers;
            try
            {
                var flightManagement = new FlightManagementDBContext();
                passengers = flightManagement.Passengers.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return passengers;
        }
        public IEnumerable<Passenger> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                using (var flightManagement = new FlightManagementDBContext())
                {
                    // Tính toán vị trí bắt đầu của trang hiện tại trong tập dữ liệu
                    int startIndex = (pageNumber - 1) * pageSize;

                    // Lấy dữ liệu cho trang hiện tại, với số lượng bản ghi là pageSize, bắt đầu từ vị trí startIndex
                    var passengers = flightManagement.Passengers
                        .OrderBy(p => p.Id) // Sắp xếp theo Id hoặc trường nào đó để đảm bảo thứ tự không thay đổi
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();

                    return passengers;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paged passengers: {ex.Message}");
            }
        }
        public int GetTotalCount()
        {
            using (var flightManagement = new FlightManagementDBContext())
            {
                

                return flightManagement.Passengers.Count();
            }
        }
    }
}
