using System;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gruvo.DTL;
using Gruvo.BLL;
using Gruvo.DAL.Repository;

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private BaseRepository _repository;

        public AuthController(BaseRepository repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]UserLoginModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                UserInfo userFromDB = _repository.UserDAO.GetUserByEmailAndPwd(user.Email, user.Password);

                if (userFromDB == null)
                {
                    return Unauthorized();
                }

                string token = TokenManager.GenerateToken(userFromDB.Id);

                TokenUserPairs.GetInstance().GetPairs().Add(token, userFromDB);
                Response.Cookies.Append("Gruvo", token);

                return Ok("Success!");
            }
            catch (Exception ex)
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

                _repository.UserDAO.AddUser(user.Login, user.Password, user.Email, DateTime.Now);

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