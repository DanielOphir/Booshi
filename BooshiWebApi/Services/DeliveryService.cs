using BooshiDAL;
using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooshiWebApi.Services
{
    public class DeliveryService
    {
        private readonly BooshiDBContext _context;
        public DeliveryService(BooshiDBContext context)
        {
            this._context = context;
        }
        public async Task<List<dynamic>> GetDeliveriesByIdAsync(Guid id)
        {
            var deliveries = await  (from d in _context.Deliveries
                              where d.UserId == id
                              join origin in _context.Origins on d.Id equals origin.DeliveryId
                              join destination in _context.Destinations on d.Id equals destination.DeliveryId
                              select new { Delivery = d, Origin = origin, Destination = destination }).Cast<dynamic>().ToListAsync();

            return deliveries;
        }
    }
}
