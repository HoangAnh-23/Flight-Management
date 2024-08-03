using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IBaggageRepository
    {
        IEnumerable<Baggage> GetAll();
        Baggage GetByID(int Id);
        void insert(Baggage baggage);
        void update(Baggage baggage);
        void delete(Baggage baggage);
        IEnumerable<Baggage> SearchByWeight(decimal weight);
        int GetTotalCount();
        IEnumerable<Baggage> GetPaged(int pageNumber, int pageSize);
    }
}
