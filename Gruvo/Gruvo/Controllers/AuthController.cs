using System;
using System.Collections.Generic;
using System.Linq;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using Gruvo.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Gruvo.Data;

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]UserLoginModel user)
        {
            try
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

                var token = TokenManager.GenerateToken(userFromDB.Id);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);

                Response.Cookies.Append("gruvo_token", token, options);

                TokenUserPairs.GetInstance().GetPairs().Add(token, userFromDB);

                // TODO: Redirect

                return Ok(token);
            }
            catch
            {               
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost, Route("signup")]
        public IActionResult Signup([FromBody]UserSignUpModel user)
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

        [HttpPost, Authorize]
        public IActionResult TestAuth()
        {
            return Ok("Authorized!");
        }
    }
}