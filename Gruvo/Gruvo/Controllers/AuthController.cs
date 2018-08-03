using System;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gruvo.Data;
using Gruvo.DTL;
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
                if (user == null)
                {
                    return BadRequest();
                }

                UserInfo userFromDB = Store.MsSQL().UserDAO.GetUserByEmailAndPwd(user.Email, user.Password);

                if (userFromDB == null)
                {
                    return Unauthorized();
                }

                string token = TokenManager.GenerateToken(userFromDB.Id);

                TokenUserPairs.GetInstance().GetPairs().Add(token, userFromDB);
                Response.Cookies.Append("Gruvo", token);

                return Ok("Success!");
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Policy = "GruvoCookie"), Route("logout")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("Gruvo");

            return Redirect("/");
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

                Store.MsSQL().UserDAO.AddUser(user.Login, user.Password, user.Email, DateTime.Now);

                // TODO: Redirect

                return Ok("Success!");
            }
            catch(Exception ex)
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