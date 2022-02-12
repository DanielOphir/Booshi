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
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly BooshiDBContext _context;

        public DeliveryPersonRepository(BooshiDBContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Adding new delivery person to the database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Delivery person</returns>
        public async Task<DeliveryPerson> AddDeliveryPersonAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            user.RoleId = 2;
            var deliveryPerson = await _context.DeliveryPeople.FirstOrDefaultAsync(x => x.UserId == id);
            if (deliveryPerson != null)
            {
                deliveryPerson.IsActiveDeliveryPerson = true;
                await _context.SaveChangesAsync();
                return deliveryPerson;
            }
            deliveryPerson = new DeliveryPerson { UserId = id, IsActiveDeliveryPerson = true };
            await _context.DeliveryPeople.AddAsync(deliveryPerson);
            await _context.SaveChangesAsync();
            return deliveryPerson;
        }

        /// <summary>
        /// Remove delivery person to the database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Boolean whether the action succeed or failed</returns>
        public async Task<bool> RemoveDeliveryPersonAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return false;
            }
            user.RoleId = 3;
            var deliveryPerson = await _context.DeliveryPeople.FirstOrDefaultAsync(x => x.UserId == id);
            if (deliveryPerson == null)
            {
                return false;
            }
            deliveryPerson.IsActiveDeliveryPerson = false;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Gets all of the delivery persons
        /// </summary>
        /// <returns>IQueryable form of all of the deliveries persons</returns>
        public IQueryable<dynamic> GetAllDeliveryPeopleAsync()
        {
            var deliveryPeople = (from _deliverypeople in _context.DeliveryPeople
                                  join _user in _context.Users on _deliverypeople.UserId equals _user.Id
                                  join _userDetails in _context.UsersDetails on _deliverypeople.UserId equals _userDetails.UserId
                                  select new { User = new FullUser(_user, _userDetails), isActiveDeliveryPerson = _deliverypeople.IsActiveDeliveryPerson }).Cast<dynamic>();
            return deliveryPeople;
        }

    }
}
