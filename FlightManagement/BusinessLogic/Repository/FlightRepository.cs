using DataAccess.Models;
using BusinessLogic.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;

namespace BusinessLogic.Repository
{
    public class FlightRepository : IFlightRepository
    {
        public void delete(Flight flight)=>FlightDAO.Instance.Remove(flight);

        public IEnumerable<Flight> GetAll() => FlightDAO.Instance.GetAllList();

        public Flight GetByID(int Id) => FlightDAO.Instance.GetByID(Id);

   

        public IEnumerable<Flight> GetPaged(int pageNumber, int pageSize)=> FlightDAO.Instance.GetPaged(pageNumber, pageSize);

        public IEnumerable<FlightStatistics> GetStatistics() => FlightDAO.Instance.GetStatistics();
        public int GetTotalCount()=> FlightDAO.Instance.GetTotalCount();

        public void insert(Flight flight) => FlightDAO.Instance.Add(flight);

        public IEnumerable<Flight> SearchByName(string search) => FlightDAO.Instance.SearchByName(search);

        public void update(Flight flight) => FlightDAO.Instance.Update(flight);
    }
}
