using BooshiDAL;
using BooshiDAL.Interfaces;
using BooshiDAL.Models;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{

    public class DeliveriesController : BaseController
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly IDeliveryRepository _deliveryRepo;

        public DeliveriesController(IJwtService jwtService, IUserRepository userRepo, IDeliveryRepository deliveryRepo)
        {
            this._jwtService = jwtService;
            this._userRepo = userRepo;
            this._deliveryRepo = deliveryRepo;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDelivery(int id)
        {
            var delivery = await _deliveryRepo.GetDeliveryByIdAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        [Authorize(Roles = "Admin, DeliveryPerson")]
        [HttpGet("page/{pageNum}")]
        public async Task<IActionResult> GetAllDeliveriesAsync(int pageNum)
        {
            var deliveries = await _deliveryRepo.GetAllDeliveriesByPageAsync(pageNum);
            var totalDeliveries = _deliveryRepo.GetTotalDeliveriesCount();
            if (deliveries.Count() < 1)
                return NoContent();
            return Ok(new { deliveries, totalDeliveries });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/page/{pageNum}")]
        public async Task<IActionResult> GetUserDeliveriesByIdAsync(Guid id, int pageNum)
        {
            if (await _userRepo.isUserExistsAsync(id) == false)
                return NotFound("No user found");
            var deliveries = await _deliveryRepo.GetUserDeliveriesByPageNum(id, pageNum);
            var totalCount = _deliveryRepo.GetDeliveryCountByUserId(id);
            if (deliveries.Count() < 1)
                return NoContent();
            return Ok(new { deliveries, totalCount});
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("/deliveryperson/{id}")]
        public async Task<IActionResult> GetDeliveriesByDeliveryPersonAsync(Guid id)
        {
            var deliveries = await _deliveryRepo.GetDeliveriesByDeliveryPerson(id);
            if (deliveries == null)
            {
                return BadRequest(new { message = "No delivery person found" });
            }
            if (deliveries.Count() < 1)
            {
                return NoContent();
            }
            return Ok(deliveries);
        }

        [HttpGet("user/page/{pageNumber}")]
        public async Task<IActionResult> GetUserDeliveriesAsync(int pageNumber)
        {
            Guid userId = _jwtService.GetUserByTokenAsync(Request);
            var deliveries = await _deliveryRepo.GetUserDeliveriesByPagesAsync(userId, pageNumber);
            if (deliveries.Count() < 1)
                return NoContent();
            var totalCount = _deliveryRepo.GetDeliveryCountByUserId(userId);
            return Ok(new { deliveries, totalDeliveries = totalCount });
        }

        [HttpGet("new-deliveries/page/{pageNumber}")]
        public async Task<IActionResult> GetNewDeliveriesAsync(int pageNumber)
        {
            var deliveries = await _deliveryRepo.GetNewDeliveriesByPagesAsync(pageNumber);
            if (deliveries.Count() < 1)
                return NoContent();
            var totalCount = _deliveryRepo.GetNewDeliveriesCount();
            return Ok(new { deliveries, totalDeliveries = totalCount });
        }

        [HttpPatch("cancel/{deliveryId}")]
        public async Task<IActionResult> CancelDeliveryAsync(int deliveryId)
        {
            var deliveryStatus = await _deliveryRepo.GetDeliveryStatusAsync(deliveryId);
            if (deliveryStatus != 1)
            {
                return BadRequest(new { message = "Can't cancel delivery that is not pending." });
            }
            await _deliveryRepo.ChangeDeliveryStatus(deliveryId, 4);
            return Ok();
        }

        [Authorize(Roles = "Admin, DeliveryPerson")]
        [HttpPatch("change-status/{deliveryId}")]
        public async Task<IActionResult> ChangeDeliveryStatusAsync(int deliveryId, [FromBody] int statusId)
        {
            var delivery = await _deliveryRepo.GetDeliveryByIdAsync(deliveryId);
            if (delivery == null)
            {
                return BadRequest(new { message = "Delivery could not be found." });
            }
            await _deliveryRepo.ChangeDeliveryStatus(deliveryId,statusId);
            return Ok();
        }

        [Authorize(Roles ="Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateDelivery(FullDelivery delivery)
        {
            var updatedDelivery = await _deliveryRepo.UpdateDeliveryAsync(delivery);
            if (updatedDelivery == null)
            {
                return NotFound("Delivery not found");
            }
            return Ok(updatedDelivery);
        }

        [Authorize(Roles = "Admin, DeliveryPerson")]
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateDeliveryStatusAsync([FromForm] int deliveryId, [FromForm] int statusId)
        {
            await _deliveryRepo.ChangeDeliveryStatus(deliveryId, statusId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddDeliveryAsync(FullDelivery fullDelivery)
        {
            try
            {
               var delivery = await _deliveryRepo.AddDeliveryAsync(fullDelivery);
                return Created("/", delivery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDeliveryAsync([FromBody]int id)
        {
            var success = await _deliveryRepo.DeleteDeliveryAsync(id);
            if (!success)
                return BadRequest("Could not delete.");
            return Ok(success);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("status/{statusId}/page/{pageNum}")]
        public async Task<IActionResult> GetDeliveriesByStatusId(int statusId, int pageNum)
        {
            var deliveries = await _deliveryRepo.GetDeliveriesByStatusId(statusId, pageNum);
            if (deliveries.Count() < 1)
                return NoContent();
            var totalDeliveries = _deliveryRepo.GetDeliveryCountByStatusId(statusId);
            return Ok(new { deliveries, totalDeliveries });
        }
    }
}
