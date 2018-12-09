using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtSample.DataContext;
using JwtSample.DataContext.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtSample.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private IServiceProvider _services;
        public AuthController(IServiceProvider services) {
            _services = services;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] User user)
        {
            user.SignUpDate = DateTime.UtcNow.Date;
            var db = (UsersContext)_services.GetService(typeof(UsersContext));
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return Ok(new { Message = "User added"});
        }
    }
}