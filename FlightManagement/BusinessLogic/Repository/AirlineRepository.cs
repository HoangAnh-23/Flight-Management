using BusinessLogic.Dao;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class AirlineRepository : IAirlineRepository
    {
      

        public Airline GetAirlineByID(int airlineId)=>AirlineDAO.Instance.GetAirlineByID(airlineId);

        public IEnumerable<Airline> GetAirlines() => AirlineDAO.Instance.GetAirlineList();

        public void insertAirline(Airline airline)=>AirlineDAO.Instance.AddAirline(airline);

        public IEnumerable<Airline> SearchByName(string search)=>AirlineDAO.Instance.SearchByName(search);

        public void updateAirline(Airline airline)=>AirlineDAO.Instance.Update(airline);
        public void deleteAirline(Airline airline)=>AirlineDAO.Instance.Remove(airline);

        public int GetTotalCount()=> AirlineDAO.Instance.GetTotalCount();

        public IEnumerable<Airline> GetPaged(int pageNumber, int pageSize) => AirlineDAO.Instance.GetPaged(pageNumber, pageSize);
    }
}
