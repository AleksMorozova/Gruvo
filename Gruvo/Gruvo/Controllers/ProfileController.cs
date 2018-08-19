using System;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Gruvo.DTL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy = "GruvoCookie")]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private BaseRepository _repository;

        public ProfileController(BaseRepository repository)
        {
            _repository = repository;
        }

        [Route("userInfo")]
        [HttpGet]
        public IActionResult GetUserInfo()
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

        [Route("userInfo/{id}")]
        [HttpGet]
        public IActionResult GetUserInfo(long id)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                Models.UserInfo userInfo = _repository.UserDAO.GetUser(id);
                AnotherUser anotherUser = new AnotherUser()
                { Login = userInfo.Login,
                    Id = userInfo.Id,
                    About = userInfo.About,
                    Bday = userInfo.Bday,
                    RegDateTime = userInfo.RegDateTime,
                    IsSubscribed = _repository.UserDAO.IsSubscribed(userid, id)
                };
                return Ok(anotherUser);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("subscribers")]
        [HttpGet]
        public IActionResult GetSubscribers()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetSubscribers(userid));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
        [Route("subscriptions")]
        [HttpGet]
        public IActionResult GetSubscriptions()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetSubscriptions(userid));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
        [Route("userTweets")]
        [HttpGet]
        public IActionResult GetUserTweets()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.TweetDAO.GetUserPosts(userid));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}