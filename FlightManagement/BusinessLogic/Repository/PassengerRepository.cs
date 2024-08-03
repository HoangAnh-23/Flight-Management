using DataAccess.Models;
using BusinessLogic.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class PassengerRepository : IPassengerRepository
    {
        public void delete(Passenger passenger)=>PassengerDAO.Instance.Remove(passenger);

        public IEnumerable<Passenger> GetAll() => PassengerDAO.Instance.GetAllList();

        public Passenger GetByID(int Id) => PassengerDAO.Instance.GetByID(Id);

        public IEnumerable<Passenger> GetPaged(int pageNumber, int pageSize)=>PassengerDAO.Instance.GetPaged(pageNumber, pageSize);

        public int GetTotalCount()=>PassengerDAO.Instance.GetTotalCount();

        public void insert(Passenger passenger) => PassengerDAO.Instance.Add(passenger);

        public IEnumerable<Passenger> SearchByName(string search)=>PassengerDAO.Instance.SearchByName(search);

        public void update(Passenger passenger) => PassengerDAO.Instance.Update(passenger);
    }
}
