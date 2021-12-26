using BooshiDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : Controller
    {
        private readonly BooshiDBContext _context;

        public DeliveryPersonController(BooshiDBContext context)
        {
            this._context = context;
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
    }
}
