using BusinessLogic.Dao;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class AirportRepository : IAirportRepository
    {
        public void delete(Airport airport)=>AirportDAO.Instance.Remove(airport);

        public IEnumerable<Airport> GetAll() => AirportDAO.Instance.GetAllList();

        public Airport GetByID(int Id) => AirportDAO.Instance.GetByID(Id);

        public IEnumerable<Airport> GetPaged(int pageNumber, int pageSize)=> AirportDAO.Instance.GetPaged(pageNumber, pageSize);

        public int GetTotalCount()=> AirportDAO.Instance.GetTotalCount();   

        public void insert(Airport airport) => AirportDAO.Instance.Add(airport);

        public IEnumerable<Airport> SearchByName(string search) => AirportDAO.Instance.SearchByName(search);

        public void update(Airport airport) => AirportDAO.Instance.Update(airport);
    }
}
