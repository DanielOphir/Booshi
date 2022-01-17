using BooshiDAL;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : Controller
    {
        private readonly BooshiDBContext _context;
        private readonly JwtService _jwtService;

        public DeliveryPersonController(BooshiDBContext context, JwtService jwtService)
        {
            this._context = context;
            this._jwtService = jwtService;
        }
        [HttpPost]
        public async Task<IActionResult> AddDeliveryPersonAsync(Guid id)
        {
            var deliveryPerson = await _context.AddDeliveryPersonAsync(id);
            if (deliveryPerson == null)
            {
                return BadRequest("No user found");
            }
            return Created("",deliveryPerson);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveDeliveryPersonAsync(Guid id)
        {
            var success = await _context.RemoveDeliveryPersonAsync(id);
            if (!success)
            {
                return BadRequest("Something went wrong");
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveryPeopleAsync()
        {
            var deliveryPeople = await _context.GetAllDeliveryPeopleAsync().ToListAsync();
            if (deliveryPeople.Count < 1)
            {
                return NoContent();
            }
            return Ok(deliveryPeople);
        }

        [HttpGet("{isActive}")]
        public async Task<IActionResult> GetDeliveryPeopleByActivityAsync(bool isActive)
        {
            var deliveryPeople = await _context.GetDeliveryPeopleByActivityAsync(isActive);
            if (deliveryPeople.Count < 1)
            {
                return NoContent();
            }
            return Ok(deliveryPeople);
        }


        [HttpPatch("asign-self")]
        public async Task<IActionResult> AsignSelfDeliveryPerson([FromBody]int deliveryId)
        {
            var delivery = await _context.GetDeliveryByIdAsync(deliveryId);
            if (delivery != null && delivery.DeliveryPersonId != null)
                return BadRequest(new { message = "This delivery is already asigned to other delivery person." });
            Guid deliveryPersonId = _jwtService.GetUserByTokenAsync(Request);
            await _context.AsignDeliveryPerson(deliveryId, deliveryPersonId);
            return Ok(delivery);
        }
    }
}   
