using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtSample.DataContext.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtSample.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        public AuthController() { }
        [HttpPost("signup")]
        public IActionResult SignUp([FromForm] User user)
        {
            return View();
        }
    }
}