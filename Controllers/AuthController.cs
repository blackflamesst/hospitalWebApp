using Hospital.Context;
using Hospital.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HospitalContext _context;

        public AuthController(HospitalContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Login == request.Login && c.Password == request.Password);

            if (customer == null)
            {
                return Unauthorized(new { Message = "Check your login or password!" });
            }

            return Ok(new { Customer_Id = customer.Id, Message = "Successfully!" });
        }
    }

    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
