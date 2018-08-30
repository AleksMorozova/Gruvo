using System;
using Gruvo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gruvo.DTL;
using Gruvo.BLL;
using Gruvo.DAL.Repository;

namespace Gruvo.Controllers
{
    [Authorize(Policy = "GruvoCookie")]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private BaseRepository _repository;

        public SettingsController(BaseRepository repository)
        {
            _repository = repository;
        }
        
        [Route("userEditGetInfo")]
        [HttpGet]
        public IActionResult GetUserEditInfo()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetUser(userid));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        
        [HttpPost, Route("editInfo")]
        public IActionResult EditInfo([FromBody]UserEditModel user)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;

                if (user == null)
                {
                    return BadRequest();
                }

                _repository.UserDAO.UpdateUserInfo(userid, user.Login, user.Email,user.About, user.Bday);

                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost, Route("editPassword")]
        public IActionResult EditPassword([FromBody]PasswordEditModel passwords)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;

                if (passwords == null)
                {
                    return BadRequest();
                }
                string currentPassword = _repository.UserDAO.GetUserPassword(userid);

                if(currentPassword != passwords.OldPassword)
                {
                    return BadRequest("Wrong password input");
                }

                _repository.UserDAO.UpdatePassword(userid, passwords.NewPassword);

                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}
