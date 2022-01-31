using BooshiDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Interfaces
{
    public interface IDeliveryPersonRepository
    {
        public  Task<DeliveryPerson> AddDeliveryPersonAsync(Guid id);
        public  Task<bool> RemoveDeliveryPersonAsync(Guid id);
        public IQueryable<dynamic> GetAllDeliveryPeopleAsync();
        public  Task<IEnumerable<FullUser>> GetDeliveryPeopleByActivityAsync(bool isActive);

    }
}
