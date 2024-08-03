using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Repository
{
    public interface IAirlineRepository
    {
        IEnumerable<Airline> GetAirlines();
        Airline GetAirlineByID(int airlineId);
        void insertAirline(Airline airline);
        void updateAirline(Airline airline);
        void deleteAirline(Airline airline);
        int GetTotalCount();
        IEnumerable<Airline> SearchByName(string search);
        IEnumerable<Airline> GetPaged(int pageNumber, int pageSize);
    }
}
