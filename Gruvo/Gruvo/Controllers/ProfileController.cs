using System;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
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
        [Route("subscribersQuality")]
        [HttpGet]
        public IActionResult GetSubscribersQuality()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetSubscribersQuality(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("subscriptionsQuality")]
        [HttpGet]
        public IActionResult GetSubscriptionsQuality()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetSubscriptionsQuality(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e);
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
        [Route("userPostsQuality")]
        [HttpGet]
        public IActionResult GetUserPostsQuality()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.TweetDAO.GetUserPostsQuality(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}