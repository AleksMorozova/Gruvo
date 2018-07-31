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

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static MSSQL DataBase = new MSSQL("Data Source=INTEL;Initial Catalog=Gruvo;Integrated Security=True");

        public static List<UserInfo> Users = DataBase.UserDAO.GetUsers().ToList();
        public static Dictionary<string, UserInfo> TokenUserPairs = new Dictionary<string, UserInfo>();

        [HttpPost]
        public IActionResult Login([FromBody]UserLogin user)
        {
            if (user == null)
                return BadRequest();
            //var email = Request.Form["email"];
            //var password = Request.Form["password"];

            UserInfo userFromDB = DataBase.UserDAO.GetUserByEmailAndPwd(user.Email, user.Password);

            if (userFromDB == null)
                return Unauthorized();

            var token = TokenManager.GenerateToken(userFromDB.Id);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("gruvo_token", token, options);

            TokenUserPairs.Add(token, userFromDB);

            return Ok(token);
        }
    }
}