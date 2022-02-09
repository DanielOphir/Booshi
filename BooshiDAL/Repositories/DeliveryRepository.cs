using BooshiDAL.Interfaces;
using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly BooshiDBContext _context;

        public DeliveryRepository(BooshiDBContext context)
        {
            this._context = context;
        }

        public IQueryable<FullDelivery> GetAllDeliveriesQuery()
        {
            var deliveriesList = (from deliveries in _context.Deliveries
                                  join origins in _context.Origins on deliveries.Id equals origins.DeliveryId
                                  join destinations in _context.Destinations on deliveries.Id equals destinations.DeliveryId
                                  select new FullDelivery() { Delivery = deliveries, Origin = origins, Destination = destinations });
            return deliveriesList;
        }

        public async Task<IEnumerable<FullDelivery>> GetAllDeliveriesByPageAsync(int pageNum)
        {
            return await GetAllDeliveriesQuery().OrderByDescending(delivery => delivery.Delivery.Created).Skip(pageNum * 10 - 10).Take(10).ToListAsync();
        }

        public async Task<Delivery> AsignDeliveryPerson(int deliveryId, Guid deliveryPersonId)
        {
            var deliveryPerson = await _context.Users.Where(user => user.RoleId == 2).FirstOrDefaultAsync(dp => dp.Id == deliveryPersonId);
            if (deliveryPerson == null)
            {
                throw new Exception("Delivery person doesn't exists");
            }
            var delivery = await GetDeliveryByIdAsync(deliveryId);
            if (delivery == null)
            {
                throw new Exception("Delivery doesn't exists");
            }
            delivery.Delivery.DeliveryPersonId = deliveryPersonId;
            await _context.SaveChangesAsync();
            return delivery.Delivery;
        }

        public async Task<FullDelivery> GetDeliveryByIdAsync(int deliveryId)
        {
            return await GetAllDeliveriesQuery().FirstOrDefaultAsync(delivery => delivery.Delivery.Id == deliveryId);
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

            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
         
            var origin = new Origin
            {
                DeliveryId = delivery.Id,
                Email = fullDelivery.Origin.Email,
                FirstName = fullDelivery.Origin.FirstName,
                LastName = fullDelivery.Origin.LastName,
                PhoneNumber = fullDelivery.Origin.PhoneNumber,
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
            await _context.Origins.AddAsync(origin);
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
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
                var delivery = await _context.Deliveries.FirstOrDefaultAsync(x => x.Id == id);
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
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
        public async Task<IEnumerable<FullDelivery>> GetUserDeliveriesByPageNum(Guid id, int pageNum)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.UserId == id).OrderByDescending(delivery => delivery.Delivery.Created).Skip(pageNum * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }

        public int GetDeliveryCountByUserId(Guid id)
        {
            return GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.UserId == id).Count();
        }
        public int GetDeliveryCountByStatusId(int statusId)
        {
            return GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryStatusId == statusId).Count();
        }

        public int GetTotalDeliveriesCount()
        {
            return GetAllDeliveriesQuery().Count();
        }

        /// <summary>
        /// Get deliveries of certain user by page number
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="pageNumber">Number of page in paginator</param>
        /// <returns>List of deliveries</returns>
        public async Task<IEnumerable<FullDelivery>> GetUserDeliveriesByPagesAsync(Guid id, int pageNumber)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.UserId == id).OrderByDescending(x => x.Delivery.Created).Skip(pageNumber * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }

        public async Task<IEnumerable<FullDelivery>> GetNewDeliveriesByPagesAsync(int pageNumber)
        {
            var deliveries = GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryStatusId == 1 && delivery.Delivery.DeliveryPersonId == null).OrderBy(x => x.Delivery.Created).Skip(pageNumber * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }

        public async Task<bool> ChangeDeliveryStatus(int deliveryId, int statusId)
        {
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(delivery => delivery.Id == deliveryId);
            if (delivery == null)
            {
                return false;
                throw new ArgumentException($"Delivery #{deliveryId} doesn't exist");
            }
            var status = await _context.DeliveryStatuses.FirstOrDefaultAsync(status => status.Id == statusId);
            if (status == null)
            {
                return false;
                throw new ArgumentException($"Status number {statusId} doesn't exist");
            }
            delivery.DeliveryStatusId = statusId;
            await _context.SaveChangesAsync();
            return true;
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
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(delivery => delivery.Id == deliveryId);
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
        public async Task<IEnumerable<FullDelivery>> GetDeliveriesByDeliveryPerson(Guid id)
        {
            if (await _context.DeliveryPeople.FirstOrDefaultAsync(x => x.UserId == id) == null)
                return null;
            return await GetAllDeliveriesQuery().Where(delivery => delivery.Delivery.DeliveryPersonId == id).ToListAsync();
        }

        public async Task<IEnumerable<FullDelivery>> GetDeliveriesByStatusId(int statusId, int pageNum)
        {
            return await GetAllDeliveriesQuery().Where(x => x.Delivery.DeliveryStatusId == statusId).OrderByDescending(delivery => delivery.Delivery.Created).Skip(pageNum * 10 - 10).Take(10).ToListAsync();
        }

        public async Task<FullDelivery> UpdateDeliveryAsync(FullDelivery delivery)
        {
            var foundDelivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.Id == delivery.Delivery.Id);
            var foundOrigin = await _context.Origins.FirstOrDefaultAsync(o => o.DeliveryId == delivery.Delivery.Id);
            var foundDestination = await _context.Destinations.FirstOrDefaultAsync(d => d.DeliveryId == delivery.Delivery.Id);
            if (foundDelivery == null || foundOrigin == null || foundDestination == null)
                return null;
            foundDelivery.Notes = delivery.Delivery.Notes;
            foundDelivery.DeliveryStatusId = delivery.Delivery.DeliveryStatusId;
            foundOrigin.Street = delivery.Origin.Street;
            foundOrigin.City = delivery.Origin.City;
            foundOrigin.ZipCode = delivery.Origin.ZipCode;
            foundOrigin.Email = delivery.Origin.Email;
            foundOrigin.FirstName = delivery.Origin.FirstName;
            foundOrigin.LastName = delivery.Origin.LastName;
            foundOrigin.PhoneNumber = delivery.Origin.PhoneNumber;
            foundDestination.Street = delivery.Destination.Street;
            foundDestination.City = delivery.Destination.City;
            foundDestination.ZipCode = delivery.Destination.ZipCode;
            foundDestination.Email = delivery.Destination.Email;
            foundDestination.FirstName = delivery.Destination.FirstName;
            foundDestination.LastName = delivery.Destination.LastName;
            foundDestination.PhoneNumber = delivery.Destination.PhoneNumber;
            await _context.SaveChangesAsync();
            return new FullDelivery { Delivery = foundDelivery, Origin = foundOrigin, Destination = foundDestination };
        }

        public async Task<IEnumerable<FullDelivery>> GetDeliveriesByDeliveryPerson(Guid id, int pageNumber)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.DeliveryPersonId == id).OrderByDescending(x => x.Delivery.Created).Skip(pageNumber * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }
    }
}
