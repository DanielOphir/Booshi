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

        public async Task<Delivery> AsignDeliveryPerson(int deliveryId, Guid deliveryPersonId)
        {
            var deliveryPerson = await this.Users.Where(user=> user.RoleId == 2).FirstOrDefaultAsync(dp => dp.Id == deliveryPersonId);
            if (deliveryPerson == null)
            {
                throw new Exception("Delivery person doesn't exists");
            }
            var delivery = await this.GetDeliveryByIdAsync(deliveryId);
            if (delivery == null)
            {
                throw new Exception("Delivery doesn't exists");
            }
            delivery.DeliveryPersonId = deliveryPersonId;
            await this.SaveChangesAsync();
            return delivery;
        }
        
        public async Task<Delivery> GetDeliveryByIdAsync(int deliveryId)
        {
            return await this.Deliveries.FirstOrDefaultAsync(delivery => delivery.Id == deliveryId);
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
                Notes = fullDelivery.Delivery.Notes,
                DeliveryStatusId = 1,
                DeliveryPersonId = null
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
                ZipCode = fullDelivery.Destination.ZipCode,
                FirstName = fullDelivery.Destination.FirstName,
                LastName = fullDelivery.Destination.LastName,
                Email = fullDelivery.Destination.Email,
                PhoneNumber = fullDelivery.Destination.PhoneNumber
            };
            await this.Origins.AddAsync(origin);
            await this.Destinations.AddAsync(destination);
            await this.SaveChangesAsync();
            return new FullDelivery { Delivery = delivery, Origin = origin, Destination = destination };
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

        /// <summary>
        /// Get all deliveries of cetrain user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>List of deliveries of the user</returns>
        public async Task<List<FullDelivery>> GetUserDeliveriesByPageNum(Guid id, int pageNum)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.UserId == id).Skip(pageNum * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }

        public int GetDeliveryCountByUserId(Guid id)
        {
            return GetAllDeliveriesQuery().Where(X => X.Delivery.UserId == id).Count();
        }

        /// <summary>
        /// Get deliveries of certain user by page number
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="pageNumber">Number of page in paginator</param>
        /// <returns>List of deliveries</returns>
        public async Task<List<FullDelivery>> GetUserDeliveriesByPagesAsync(Guid id, int pageNumber)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.UserId == id).OrderByDescending(x => x.Delivery.Created).Skip(pageNumber * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }

        public async Task<List<FullDelivery>> GetNewDeliveriesByPagesAsync(int pageNumber)
        {
            var deliveries = GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryStatusId == 1 && delivery.Delivery.DeliveryPersonId == null).OrderBy(x => x.Delivery.Created).Skip(pageNumber * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }

        public async Task<bool> ChangeDeliveryStatus(int deliveryId, int statusId)
        {
            var delivery = await this.Deliveries.FirstOrDefaultAsync(delivery => delivery.Id == deliveryId);
            if (delivery == null)
            {
                return false;
                throw new ArgumentException($"Delivery #{deliveryId} doesn't exist");
            }
            var status = await this.DeliveryStatuses.FirstOrDefaultAsync(status => status.Id == statusId);
            if (status == null)
            {
                return false;
                throw new ArgumentException($"Status number {statusId} doesn't exist");
            }
            delivery.DeliveryStatusId = statusId;
            await this.SaveChangesAsync();
            return true;
        }

        public int GetUserDeliveriesCount(Guid id)
        {
            return GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.UserId == id).Count();
        }

        public int GetNewDeliveriesCount()
        {
            return GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryStatusId == 1 && delivery.Delivery.DeliveryPersonId == null).Count();
        }

        public int GetDeliveryPersonDeliveriesCount(Guid id)
        {
            return GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryPersonId == id).Count();
        }

        public async Task<int> GetDeliveryStatusAsync(int deliveryId)
        {
            var delivery = await this.Deliveries.FirstOrDefaultAsync(delivery => delivery.Id == deliveryId);
            if (delivery == null)
            {
                throw new ArgumentException($"Delivery #{deliveryId} doesn't exist");
            }
            return delivery.DeliveryStatusId;
        }

        /// <summary>
        /// Get all deliveries of cetrain delivery person
        /// </summary>
        /// <param name="id">Delivery person id</param>
        /// <returns>Return all deliveries of cetrain delivery person, if not found return null</returns>
        public async Task<List<FullDelivery>> GetDeliveriesByDeliveryPerson(Guid id)
        {
            if (await this.DeliveryPeople.FirstOrDefaultAsync(x => x.UserId == id) == null)
                return null;
            return await this.GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryPersonId == id).ToListAsync();
        }

        public async Task<List<FullDelivery>> GetDeliveriesByStatusId(int statusId) {
            if (await this.DeliveryStatuses.FirstOrDefaultAsync(x => x.Id == statusId) == null)
                return null;
            return await this.GetAllDeliveriesQuery().Where(x => x.Delivery.DeliveryStatusId == statusId).ToListAsync();
        }
        
    }
}
