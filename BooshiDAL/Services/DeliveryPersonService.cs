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

        public async Task<DeliveryPerson> AddDeliveryPersonAsync(Guid id)
        {
            var user = await this.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            user.RoleId = 2;
            var deliveryPerson = await this.DeliveryPeople.FirstOrDefaultAsync(x => x.UserId == id);
            if(deliveryPerson != null)
            {
                deliveryPerson.IsActiveDeliveryPerson = true;
                await this.SaveChangesAsync();
                return deliveryPerson;
            }
            deliveryPerson = new DeliveryPerson { UserId = id, IsActiveDeliveryPerson = true };
            await this.DeliveryPeople.AddAsync(deliveryPerson);
            await this.SaveChangesAsync();
            return deliveryPerson;
        }

        public async Task<bool> RemoveDeliveryPersonAsync(Guid id)
        {
            var user = await this.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return false;
            }
            user.RoleId = 3;
            var deliveryPerson = await this.DeliveryPeople.FirstOrDefaultAsync(x => x.UserId == id);
            if (deliveryPerson == null)
            {
                return false;
            }
            deliveryPerson.IsActiveDeliveryPerson = false;
            await this.SaveChangesAsync();
            return true;
        }

        public IQueryable<dynamic> GetAllDeliveryPeopleAsync()
        {
            var deliveryPeople = (from _deliverypeople in this.DeliveryPeople
                                  join _user in this.Users on _deliverypeople.UserId equals _user.Id
                                  join _userDetails in this.UsersDetails on _deliverypeople.UserId equals _userDetails.UserId
                                        select new { User = new FullUser(_user, _userDetails), isActiveDeliveryPerson = _deliverypeople.IsActiveDeliveryPerson }).Cast<dynamic>();
            return deliveryPeople;        
        }

        public async Task<List<FullUser>> GetDeliveryPeopleByActivityAsync(bool isActive)
        {
            var deliveryPeople = await (from _deliverypeople in this.DeliveryPeople
                                  join _user in this.Users on _deliverypeople.UserId equals _user.Id
                                  join _userDetails in this.UsersDetails on _deliverypeople.UserId equals _userDetails.UserId
                                  where _deliverypeople.IsActiveDeliveryPerson == isActive
                                        select new FullUser(_user, _userDetails)).ToListAsync();
            return deliveryPeople;
        }

        public async Task<List<FullDelivery>> GetDeliveriesByDeliveryPerson (Guid id, int pageNumber)
        {
            var deliveries = GetAllDeliveriesQuery().Where(X => X.Delivery.DeliveryPersonId == id).OrderByDescending(x => x.Delivery.Created).Skip(pageNumber * 10 - 10).Take(10);
            return await deliveries.ToListAsync();
        }
    }
}
