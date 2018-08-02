using System;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gruvo.Data;
using Gruvo.DTL;
using System.Threading.Tasks;
using Gruvo.BLL;

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            UserInfo userFromDB = AccessDatabase.MsSQL()
                                                .UserDAO
                                                .GetUserByEmailAndPwd(user.Email, user.Password);
            
            if (userFromDB == null)
            {
                return Unauthorized();
            }

            string token = TokenManager.GenerateToken(userFromDB.Id);
            
            TokenUserPairs.GetInstance().GetPairs().Add(token, userFromDB);
            Response.Cookies.Append("Gruvo", token, new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Domain = "." + Request.Host.Host,
                MaxAge = TimeSpan.FromHours(10)
            });

            return Ok("Success!" + Request.Cookies["Gruvo"]);
        }

        [HttpPost, Route("signup")]
        public IActionResult Signup([FromBody]UserSignUpModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                try
                {
                    AccessDatabase.MsSQL()
                                  .UserDAO
                                  .AddUser(user.Login, user.Password, user.Email, DateTime.Now);
                }
                catch
                {
                    return BadRequest();
                }
                // TODO: Redirect

                return Ok("Success!");
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Policy = "GruvoCookie"), Route("test")]
        public IActionResult TestAuth()
        {
            return Ok("Authorized!!!!!!!!!!!");
        }
    }
}