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

        public ProfileController(BaseRepository repository)
        {
            _repository = repository;
        }

        [Route("userInfo/{id?}")]
        [HttpGet]
        public IActionResult GetUserInfo(long? id)
        {
            UserInfo user = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;

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
        [Route("subscribersCount")]
        [HttpGet]
        public IActionResult GetSubscribersQuality()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetSubscribersCount(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("subscriptionsCount")]
        [HttpGet]
        public IActionResult GetSubscriptionsQuality()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.UserDAO.GetSubscriptionsCount(userid));
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
        [Route("userPostsCount")]
        [HttpGet]
        public IActionResult GetUserPostsQuality()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.TweetDAO.GetUserPostsCount(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("postTweet")]
        [HttpPost]
        public IActionResult PostTweet([FromBody] string message)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;

                _repository.TweetDAO.AddPost(userid, message, DateTime.Now);

                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        [Route("deleteTweet")]
        [HttpPost]
        public IActionResult DeleteTweet([FromBody] long id)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;

                _repository.TweetDAO.DeletePost(id);

                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

    }
}