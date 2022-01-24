using BooshiDAL;
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

    [ApiController]
    [Route("/api/[controller]")]
    public class DeliveriesController : Controller
    {
        private readonly BooshiDBContext _context;
        private readonly JwtService _jwtService;

        public DeliveriesController(BooshiDBContext context, JwtService jwtService)
        {
            this._context = context;
            this._jwtService = jwtService;
        }

        [Authorize(Roles = "Admin, DeliveryPerson")]
        [HttpGet]
        public async Task<IActionResult> GetAllDeliveriesAsync()
        {
            var deliveries = await _context.GetAllDeliveriesAsync();
            if (deliveries.Count < 1)
                return NoContent();
            return Ok(deliveries);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/page/{pageNum}")]
        public async Task<IActionResult> GetUserDeliveriesByIdAsync(Guid id, int pageNum)
        {
            if (await _context.isUserExistsAsync(id) == false)
                return NotFound("No user found");
            var deliveries = await _context.GetUserDeliveriesByPageNum(id, pageNum);
            var totalCount = _context.GetDeliveryCountByUserId(id);
            if (deliveries.Count < 1)
                return NoContent();
            return Ok(new { deliveries, totalCount});
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("/deliveryperson/{id}")]
        public async Task<IActionResult> GetDeliveriesByDeliveryPersonAsync(Guid id)
        {
            var deliveries = await _context.GetDeliveriesByDeliveryPerson(id);
            if (deliveries == null)
            {
                return BadRequest(new { message = "No delivery person found" });
            }
            if (deliveries.Count < 1)
            {
                return NoContent();
            }
            return Ok(deliveries);
        }

        [HttpGet("user/page/{pageNumber}")]
        public async Task<IActionResult> GetUserDeliveriesAsync(int pageNumber)
        {
            Guid userId = _jwtService.GetUserByTokenAsync(Request);
            var deliveries = await _context.GetUserDeliveriesByPagesAsync(userId, pageNumber);
            if (deliveries.Count < 1)
                return NoContent();
            var totalCount = _context.GetUserDeliveriesCount(userId);
            return Ok(new { deliveries, totalDeliveries = totalCount });
        }

        [HttpGet("new-deliveries/page/{pageNumber}")]
        public async Task<IActionResult> GetNewDeliveriesAsync(int pageNumber)
        {
            var deliveries = await _context.GetNewDeliveriesByPagesAsync(pageNumber);
            if (deliveries.Count < 1)
                return NoContent();
            var totalCount = _context.GetNewDeliveriesCount();
            return Ok(new { deliveries, totalDeliveries = totalCount });
        }

        [HttpPatch("cancel/{deliveryId}")]
        public async Task<IActionResult> CancelDeliveryAsync(int deliveryId)
        {
            var deliveryStatus = await _context.GetDeliveryStatusAsync(deliveryId);
            if (deliveryStatus != 1)
            {
                return BadRequest(new { message = "Can't cancel delivery that is not pending." });
            }
            await _context.ChangeDeliveryStatus(deliveryId, 4);
            return Ok();
        }

        [Authorize(Roles = "Admin, DeliveryPerson")]
        [HttpPatch("change-status/{deliveryId}")]
        public async Task<IActionResult> ChangeDeliveryStatusAsync(int deliveryId, [FromBody] int statusId)
        {
            var delivery = await _context.GetDeliveryByIdAsync(deliveryId);
            if (delivery == null)
            {
                return BadRequest(new { message = "Delivery could not be found." });
            }
            delivery.DeliveryStatusId = statusId;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin, DeliveryPerson")]
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateDeliveryStatusAsync([FromForm] int deliveryId, [FromForm] int statusId)
        {
            await _context.ChangeDeliveryStatus(deliveryId, statusId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddDeliveryAsync(FullDelivery fullDelivery)
        {
            try
            {
               var delivery = await _context.AddDeliveryAsync(fullDelivery);
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
            var success = await _context.DeleteDeliveryAsync(id);
            if (!success)
                return BadRequest("Could not delete.");
            return Ok(success);
        }
    }
}
