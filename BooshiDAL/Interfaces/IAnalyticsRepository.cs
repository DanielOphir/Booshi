using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Interfaces
{
    public interface IAnalyticsRepository
    {
        public Task<Dictionary<int, int>> GetNewUsersByMonth();
        public Task<Dictionary<int, int>> GetDeliveriesByMonth();
    }
}
