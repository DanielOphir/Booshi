using BooshiDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly BooshiDBContext _context;

        public AnalyticsRepository(BooshiDBContext context)
        {
            this._context = context;
        }

        private Dictionary<int, int> Monthify(Dictionary<int, int> months)
        {
            var rtn = new Dictionary<int, int>();
            for (int i = 1; i <= 12; i++)
                rtn.Add(i, 0);
            foreach (var item in months)
            {
                rtn[item.Key] = item.Value;
            }
            return rtn;
        }

        public async Task<Dictionary<int, int>> GetNewUsersByMonth()
        {
            var newUsersCount = await _context.Users.Where(user => user.CreatedAt.Year == DateTime.Now.Year).GroupBy(user => user.CreatedAt.Month).Select(group => new {Month = group.Key, Count = group.Count()}).ToDictionaryAsync(group => group.Month, group => group.Count);
            return Monthify(newUsersCount);
        }


        public async Task<Dictionary<int, int>> GetDeliveriesByMonth()
        {
            var deliveriesCount = await _context.Deliveries.Where(delivery => delivery.Created.Year == DateTime.Now.Year).GroupBy(delivery => delivery.Created.Month).Select(group => new { Month = group.Key, Count = group.Count() }).ToDictionaryAsync(group => group.Month, group => group.Count);
            return Monthify(deliveriesCount);
        }

    }
}