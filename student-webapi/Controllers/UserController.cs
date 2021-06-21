using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using student_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_webapi.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);


                if (signInResult.Succeeded)
                {
                    return Ok(user.Id);
                    //return Redirect("http://localhost:5000/api/students");
                }
            }

            return NoContent();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(string username, string password)
        {
            this.HttpContext.Response.Cookies.Delete("APICookie");
            await _signInManager.SignOutAsync();
            
            return Ok();
            //return Redirect("http://localhost:5000/api/user");
        }

    }
}
