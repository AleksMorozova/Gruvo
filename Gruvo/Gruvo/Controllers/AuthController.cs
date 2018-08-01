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
            Response.Cookies.Append("Gruvo", token);

            var cookies2 = Request.Cookies.Keys;

            return Ok("Success!");
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