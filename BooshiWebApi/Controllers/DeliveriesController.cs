﻿using BooshiDAL;
using BooshiDAL.Models;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveriesAsync()
        {
            var deliveries = await _context.GetAllDeliveriesAsync();
            if (deliveries.Count < 1)
                return NoContent();
            return Ok(deliveries);

        }

        [Authorize(Roles= "Admin")]
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserDeliveriesByIdAsync(Guid id)
        {
            var deliveries = await _context.GetUserDeliveries(id);
            if (deliveries.Count < 1)
                return NoContent();
            return Ok(deliveries);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserDeliveriesAsync()
        {
            Guid userId;
            var jwtToken = Request.Cookies["jwt"];
            if (jwtToken == null)
                return StatusCode(203);
            try { userId = _jwtService.GetUserByTokenAsync(jwtToken); }
            catch (SecurityTokenExpiredException)
            {
                Response.Cookies.Delete("jwt");
                return StatusCode(203);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            var deliveries = await _context.GetUserDeliveries(userId);
            if (deliveries.Count < 1)
                return NoContent();
            return Ok(deliveries);
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
