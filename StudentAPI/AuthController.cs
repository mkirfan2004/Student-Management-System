using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Models;
using System.Linq;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest("Email already exists");

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Registered Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == loginUser.Email &&
                                     u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized("Invalid Email or Password");

            return Ok("Login Successful");
        }
    }
}