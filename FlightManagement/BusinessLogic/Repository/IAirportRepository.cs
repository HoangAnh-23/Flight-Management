using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IAirportRepository
    {
        IEnumerable<Airport> GetAll();
        Airport GetByID(int Id);
        void insert(Airport airport);
        void update(Airport airport);
        void delete(Airport airport);

        IEnumerable<Airport> SearchByName(string search);
        int GetTotalCount();
        IEnumerable<Airport> GetPaged(int pageNumber, int pageSize);
    }
}
