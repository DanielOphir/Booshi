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

        private List<int> Monthify(Dictionary<int, int> months)
        {
            var rtn = new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            foreach (var item in months)
            {
                rtn[item.Key - 1] = item.Value;
            }
            return rtn;
        }

        public async Task<List<int>> GetNewUsersByMonth()
        {
            var newUsersCount = await _context.Users.Where(user => user.CreatedAt.Year == DateTime.Now.Year).GroupBy(user => user.CreatedAt.Month).Select(group => new {Month = group.Key, Count = group.Count()}).ToDictionaryAsync(group => group.Month, group => group.Count);
            return Monthify(newUsersCount);
        }

        public async Task<List<int>> GetDeliveriesByMonth()
        {
            var deliveriesCount = await _context.Deliveries.Where(delivery => delivery.Created.Year == DateTime.Now.Year).GroupBy(delivery => delivery.Created.Month).Select(group => new { Month = group.Key, Count = group.Count() }).ToDictionaryAsync(group => group.Month, group => group.Count);
            return Monthify(deliveriesCount);
        }

        public List<int> GetReport (DateTime from, DateTime to)
        {
            List<int> report = new List<int>() {0, 0, 0, 0};
            var statuses = _context.Deliveries.Where(delivery => delivery.Created >= from && delivery.Created <= to).AsEnumerable().GroupBy(delivery => delivery.DeliveryStatusId); 
            foreach (var status in statuses)
            {
                report[status.Key - 1] = status.Count();
            }
            return report;
        }

    }
}