using BooshiDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Interfaces
{
    public interface IDeliveryRepository
    {
        IQueryable<FullDelivery> GetAllDeliveriesQuery();

        public Task<IEnumerable<FullDelivery>> GetAllDeliveriesByPageAsync(int pageNum);
        public Task<IEnumerable<FullDelivery>> GetUserDeliveriesByPageNum(Guid id, int pageNum);
        public Task<IEnumerable<FullDelivery>> GetUserDeliveriesByPagesAsync(Guid id, int pageNumber);
        public Task<IEnumerable<FullDelivery>> GetNewDeliveriesByPagesAsync(int pageNumber);
        public Task<IEnumerable<FullDelivery>> GetDeliveriesByDeliveryPerson(Guid id);
        public Task<IEnumerable<FullDelivery>> GetDeliveriesByStatusId(int statusId, int pageNum);
        public Task<IEnumerable<FullDelivery>> GetDeliveriesByDeliveryPerson(Guid id, int pageNumber);


        public Task<Delivery> AsignDeliveryPerson(int deliveryId, Guid deliveryPersonId);
        public Task<FullDelivery> GetDeliveryByIdAsync(int deliveryId);
        public Task<FullDelivery> AddDeliveryAsync(FullDelivery fullDelivery);
        public Task<FullDelivery> UpdateDeliveryAsync(FullDelivery delivery);

        public Task<bool> DeleteDeliveryAsync(int id);
        public Task<bool> ChangeDeliveryStatus(int deliveryId, int statusId);

        public int GetDeliveryCountByUserId(Guid id);
        public int GetDeliveryCountByStatusId(int statusId);
        public int GetTotalDeliveriesCount();
        public int GetNewDeliveriesCount();
        public int GetDeliveryPersonDeliveriesCount(Guid id);

        public Task<int> GetDeliveryStatusAsync(int deliveryId);
    }
}
