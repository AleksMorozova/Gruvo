using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gruvo.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static MSSQL DataBase = new MSSQL("Data Source=INTEL;Initial Catalog=Gruvo;Integrated Security=True");

        public static List<UserInfo> Users = DataBase.UserDAO.GetUsers().ToList();
        public static Dictionary<string, UserInfo> TokenUserPairs = new Dictionary<string, UserInfo>();

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]UserLoginModel user)
        {
            if (user == null)
                return BadRequest();

            UserInfo userFromDB = DataBase.UserDAO.GetUserByEmailAndPwd(user.Email, user.Password);

            if (userFromDB == null)
                return Unauthorized();

            var token = TokenManager.GenerateToken(userFromDB.Id);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("gruvo_token", token, options);

            TokenUserPairs.Add(token, userFromDB);

            // TODO: Redirect

            return Ok(token);
        }

        [HttpPost, Route("signup")]
        public IActionResult Register([FromBody]UserSignUpModel user)
        {
            if (user == null)
                return BadRequest();

            try
            {
                DataBase.UserDAO.AddUser(user.Login, user.Password, user.Email, DateTime.Now);
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