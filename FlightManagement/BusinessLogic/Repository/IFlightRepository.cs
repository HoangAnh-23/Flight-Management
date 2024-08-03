using DataAccess.DTO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IFlightRepository
    {
        IEnumerable<Flight> GetAll();
        Flight GetByID(int Id);
        int GetTotalCount();
        void insert(Flight flight);
        void update(Flight flight);
        void delete(Flight flight);
        IEnumerable<Flight> SearchByName(string search);
        IEnumerable<Flight> GetPaged(int pageNumber, int pageSize);
        IEnumerable<FlightStatistics> GetStatistics();
    }
}
