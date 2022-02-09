using BooshiDAL;
using BooshiDAL.Interfaces;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [Authorize(Roles = "DeliveryPerson, Admin")]
    public class DeliveryPersonController : BaseController
    {
        private readonly IJwtService _jwtService;
        private readonly IDeliveryRepository _deliveryRepo;
        private readonly IDeliveryPersonRepository _deliveryPersonRepo;
        private readonly IUserRepository _userRepo;

        public DeliveryPersonController(IJwtService jwtService, IDeliveryRepository deliveryRepo, IDeliveryPersonRepository deliveryPersonRepo, IUserRepository userRepo)
        {
            this._jwtService = jwtService;
            this._deliveryRepo = deliveryRepo;
            this._deliveryPersonRepo = deliveryPersonRepo;
            this._userRepo = userRepo;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDeliveryPersonAsync([FromBody]Guid id)
        {
            var deliveryPerson = await _deliveryPersonRepo.AddDeliveryPersonAsync(id);
            if (deliveryPerson == null)
            {
                return BadRequest("No user found");
            }
            return Created("",deliveryPerson);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveDeliveryPersonAsync(Guid id)
        {
            var success = await _deliveryPersonRepo.RemoveDeliveryPersonAsync(id);
            if (!success)
            {
                return BadRequest("Something went wrong");
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveryPeopleAsync()
        {
            var deliveryPeople = await _deliveryPersonRepo.GetAllDeliveryPeopleAsync().ToListAsync();
            if (deliveryPeople.Count() < 1)
            {
                return NoContent();
            }
            return Ok(deliveryPeople);
        }

        [HttpGet("{isActive}")]
        public async Task<IActionResult> GetDeliveryPeopleByActivityAsync(bool isActive)
        {
            var deliveryPeople = await _deliveryPersonRepo.GetDeliveryPeopleByActivityAsync(isActive);
            if (deliveryPeople.Count() < 1)
            {
                return NoContent();
            }
            return Ok(deliveryPeople);
        }

        [HttpGet("deliveries/page/{pageNum}")]
        public async Task<IActionResult> GetDeliveriesByDeliveryPerson(int pageNum)
        {
            var deliveryPersonId = _jwtService.GetUserByTokenAsync(Request);
            var deliveries = await _deliveryRepo.GetDeliveriesByDeliveryPerson(deliveryPersonId, pageNum);
            if (deliveries.Count() < 1)
            {
                return NoContent();
            }
            var totalDeliveries = _deliveryRepo.GetDeliveryPersonDeliveriesCount(deliveryPersonId);
            return Ok(new { deliveries, totalDeliveries });
        }

        [HttpGet("deliveries/{deliveryPersonId}/page/{pageNum}")]
        public async Task<IActionResult> GetDeliveriesByDeliveryPersonId(int pageNum, Guid deliveryPersonId)
        {
            var deliveries = await _deliveryRepo.GetDeliveriesByDeliveryPerson(deliveryPersonId, pageNum);
            if (deliveries.Count() < 1)
            {
                return NoContent();
            }
            var totalDeliveries = _deliveryRepo.GetDeliveryPersonDeliveriesCount(deliveryPersonId);
            return Ok(new { deliveries, totalCount = totalDeliveries });
        }

        [HttpGet("page/{pageNum}")]
        public async Task<IActionResult> GetDeliveryPersonsByPageNumber(int pageNum)
        {
            var deliveryPersons = await _userRepo.GetUsersByPageAsync(pageNum, 2);
            if (deliveryPersons.Count() < 1)
            {
                return NoContent();
            }
            var totalDeliveryPersons = _userRepo.GetUsersCount(2);
            return Ok(new { users = deliveryPersons, totalCount = totalDeliveryPersons });
        }

        [HttpGet("{userName}/page/{pageNum}")]
        public async Task<IActionResult> GetUsersByUserNameByPage(string userName, int pageNum)
        {
            var deliveryPersons = await _userRepo.GetUsersByUsernameByPageAsync(userName, pageNum, 2);
            if (deliveryPersons.Count() > 0)
            {
                var totalDeliveryPersons = await _userRepo.GetUsersByUsernameCount(userName, 2);
                return Ok(new { users = deliveryPersons, totalCount = totalDeliveryPersons });
            }
            return NotFound();
        }

        [HttpPatch("asign-self")]
        public async Task<IActionResult> AsignSelfDeliveryPerson([FromBody]int deliveryId)
        {
            var delivery = await _deliveryRepo.GetDeliveryByIdAsync(deliveryId);
            if (delivery != null && delivery.Delivery.DeliveryPersonId != null)
                return BadRequest(new { message = "This delivery is already asigned to other delivery person." });
            if (delivery.Delivery.DeliveryStatusId == 4)
                return BadRequest(new { message = "This delivery is cancelled." });
            Guid deliveryPersonId = _jwtService.GetUserByTokenAsync(Request);
            await _deliveryRepo.AsignDeliveryPerson(deliveryId, deliveryPersonId);
            return Ok(delivery);
        }
    }
}   
