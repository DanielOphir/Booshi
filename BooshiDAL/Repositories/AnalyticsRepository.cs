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
        /// <summary>
        /// Takes dictionary of key value pair of <Month number, Count> and makes a list of all of the months
        /// </summary>
        /// <param name="months">Dictionary of the months</param>
        /// <returns>List of count by all months</returns>
        private List<int> Monthify(Dictionary<int, int> months)
        {
            var rtn = new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            foreach (var item in months)
            {
                rtn[item.Key - 1] = item.Value;
            }
            return rtn;
        }

        /// <summary>
        /// Gets the count of new users in the current year by months
        /// </summary>
        /// <returns>List of new users count</returns>
        public async Task<List<int>> GetNewUsersByMonth()
        {
            var newUsersCount = await _context.Users.Where(user => user.CreatedAt.Year == DateTime.Now.Year).GroupBy(user => user.CreatedAt.Month).Select(group => new {Month = group.Key, Count = group.Count()}).ToDictionaryAsync(group => group.Month, group => group.Count);
            return Monthify(newUsersCount);
        }

        /// <summary>
        /// Gets the count of new deliveries in the current year by months
        /// </summary>
        /// <returns>List of new deliveries count</returns>
        public async Task<List<int>> GetDeliveriesByMonth()
        {
            var deliveriesCount = await _context.Deliveries.Where(delivery => delivery.Created.Year == DateTime.Now.Year).GroupBy(delivery => delivery.Created.Month).Select(group => new { Month = group.Key, Count = group.Count() }).ToDictionaryAsync(group => group.Month, group => group.Count);
            return Monthify(deliveriesCount);
        }

        /// <summary>
        /// Gets the new deliveries count by thier status id between dates.
        /// </summary>
        /// <param name="from">From date</param>
        /// <param name="to">To date</param>
        /// <returns>List of count of deliveries.</returns>
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