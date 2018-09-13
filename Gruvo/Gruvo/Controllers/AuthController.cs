using System;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gruvo.DTL;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using System.Net.Mail;

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private BaseRepository _repository;
        private ITokenUserPairs _tokenUserPairs;

        public AuthController(BaseRepository repository, ITokenUserPairs tokenUserPairs)
        {
            _repository = repository;
            _tokenUserPairs = tokenUserPairs;
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

                _tokenUserPairs.Pairs.Add(token, userFromDB);
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

        [HttpGet, Route("getVerificationCode")]
        public IActionResult GetVerificationCode ()
        {
            Random rnd = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] code = new char[8];
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chars[rnd.Next(0, chars.Length)];
            }

            return Ok(new String(code));
        }

        [HttpPost, Route("confirmEmail")]
        public IActionResult SentVerificationEmail ([FromBody] UserSignUpModel user)
        {
            try
            {
                if (user==null)
                {
                    return BadRequest();
                }

                UserInfo userFromDB = _repository.UserDAO.GetUserByEmail(user.Email);

                if(userFromDB!=null)
                {
                    return BadRequest("You already have an account.");
                }

                userFromDB = _repository.UserDAO.GetUserByLogin(user.Login);

                if (userFromDB != null)
                {
                    return BadRequest("The login is taken.");
                }

                MailMessage message = new MailMessage("gruvo.mail@gmail.com", user.Email);
                message.Subject = "Verify your email";
                message.Body = "Welcome to Gruvo! Your verification code is " + user.SendedVerificationCode;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("gruvo.mail@gmail.com", "gruvo725");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(message);
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }

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

                if(user.SendedVerificationCode!=user.VerificationCodeInput)
                {
                    return BadRequest("Wrong vrification code.");
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