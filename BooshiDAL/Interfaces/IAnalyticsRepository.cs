using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Interfaces
{
    public interface IAnalyticsRepository
    {
        public Task<List<int>> GetNewUsersByMonth();
        public Task<List<int>> GetDeliveriesByMonth();
        public List<int> GetReport(DateTime from, DateTime to);
    }
}
