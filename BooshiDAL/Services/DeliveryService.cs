using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL
{
    public partial class BooshiDBContext
    {
        /// <summary>
        /// Gets all the deliveries query of all users
        /// </summary>
        /// <returns>IQueryable of full detailed deliveries</returns>
        private IQueryable<FullDelivery> GetAllDeliveriesQuery()
        {
            var deliveriesList = (from deliveries in this.Deliveries
                                 join origins in this.Origins on deliveries.Id equals origins.DeliveryId
                                 join destinations in this.Destinations on deliveries.Id equals destinations.DeliveryId
                                 select new FullDelivery() { Delivery = deliveries, Origin = origins, Destination = destinations });
            return deliveriesList;
        }

        public async Task<List<FullDelivery>> GetAllDeliveriesAsync()
        {
            var deliveries = GetAllDeliveriesQuery();
            return await deliveries.ToListAsync();
        }

        /// <summary>
        /// Add delivery to the deliveries table
        /// </summary>
        /// <param name="fullDelivery">Delivery</param>
        /// <returns>returns the delivery that was added</returns>
        public async Task<FullDelivery> AddDeliveryAsync(FullDelivery fullDelivery)
        {
            var delivery = new Delivery
            {
                UserId = fullDelivery.Delivery.UserId,
                DeliveryStatusId = 1,
            };

            await this.Deliveries.AddAsync(delivery);
            try
            {
                await this.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var origin = new Origin
            {
                DeliveryId = delivery.Id,
                City = fullDelivery.Origin.City,
                Street = fullDelivery.Origin.Street,
                ZipCode = fullDelivery.Origin.ZipCode
            };
            var destination = new Destination
            {
                DeliveryId = delivery.Id,
                City = fullDelivery.Destination.City,
                Street = fullDelivery.Destination.Street,
                ZipCode = fullDelivery.Destination.ZipCode
            };
            await this.Origins.AddAsync(origin);
            await this.Destinations.AddAsync(destination);
            await this.SaveChangesAsync();
            return new FullDelivery { Delivery = delivery, Origin = origin, Destination = destination};
        }
        /// <summary>
        /// Delete the delivery that matched the id from the deliveries table.
        /// </summary>
        /// <param name="id">Id of the delivery</param>
        /// <returns>returns bool wether the delete succeed or not.</returns>
        public async Task<bool> DeleteDeliveryAsync(int id)
        {
            try
            {
                var delivery = await this.Deliveries.FirstOrDefaultAsync(x => x.Id == id);
                this.Deliveries.Remove(delivery);
                await this.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<FullDelivery>> GetUserDeliveries(Guid id)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.UserId == id);
            return await deliveries.ToListAsync();
        }
    }
}
