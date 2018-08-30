using System;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Gruvo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy = "GruvoCookie")]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private BaseRepository _repository;
        private ITokenUserPairs _tokenUserPairs;

        public ProfileController(BaseRepository repository, ITokenUserPairs tokenUserPairs)
        {
            _repository = repository;
            _tokenUserPairs = tokenUserPairs;
        }

        [Route("userInfo/{id?}")]
        [HttpGet]
        public IActionResult GetUserInfo(long? id)
        {
            UserInfo user = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (id.HasValue)
                {
                    user = _repository.UserDAO.GetUser(id.Value);
                    user.IsSubscribed = _repository.UserDAO.IsSubscribed(userid, id.Value);
                }
                else
                {
                    user = _repository.UserDAO.GetUser(userid);
                }
                return Ok(user);
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
                long userid = _tokenUserPairs.Pairs[cookie].Id;
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
                long userid = _tokenUserPairs.Pairs[cookie].Id;
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
                long userid = _tokenUserPairs.Pairs[cookie].Id;
                return Ok(_repository.TweetDAO.GetUserPosts(userid));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}