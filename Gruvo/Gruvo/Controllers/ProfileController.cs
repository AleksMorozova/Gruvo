using System;
using System.Collections.Generic;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Gruvo.DTL;
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
                    if (id.Value == userid) BadRequest("Something went wrong");
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

        [Route("subscribers/{id?}")]
        [HttpGet]
        public IActionResult GetSubscribers(long? id, [FromQuery] long? subscriberId)
        {
            IEnumerable<UserInfo> arr = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (id.HasValue)
                {
                    arr = _repository.UserDAO.GetSubscribers(id.Value, subscriberId, 5);
                    if (arr == null) BadRequest("Something went wrong");
                }
                else
                {
                    arr = _repository.UserDAO.GetSubscribers(userid, subscriberId, 5);
                }
                return Ok(arr);
            
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("subscribersCount/{id?}")]
        [HttpGet]
        public IActionResult GetNumOfSubscribers(long? id)
        {
            int numOfSubscribers;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (id.HasValue)
                {
                    numOfSubscribers = _repository.UserDAO.GetSubscribersCount(id.Value);
                }
                else
                {
                    numOfSubscribers = _repository.UserDAO.GetSubscribersCount(userid);
                }
                return Ok(numOfSubscribers);

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("subscriptions/{id?}")]
        [HttpGet]
        public IActionResult GetSubscriptions(long? id, [FromQuery] long? subscriptionId)
        {
            IEnumerable<UserInfo> arr = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id; 

                if (id.HasValue)
                {
                    arr = _repository.UserDAO.GetSubscriptions(id.Value, subscriptionId, 5);
                    if (arr == null) BadRequest("Something went wrong");
                }
                else
                {
                    arr = _repository.UserDAO.GetSubscriptions(userid, subscriptionId, 5);
                }
                return Ok(arr);
            

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("subscriptionsCount/{id?}")]
        [HttpGet]
        public IActionResult GetNumOfSubscriptions(long? id)
        {
            int numOfSubscriptions;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (id.HasValue)
                {
                    numOfSubscriptions = _repository.UserDAO.GetSubscriptionsCount(id.Value);
                }
                else
                {
                    numOfSubscriptions = _repository.UserDAO.GetSubscriptionsCount(userid);
                }
                return Ok(numOfSubscriptions);

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("userTweets/{id?}")]
        [HttpGet]
        public IActionResult GetUserTweets(long? id)
        {
            IEnumerable<ReadableTweet> arr = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (id.HasValue)
                {
                    arr = _repository.TweetDAO.GetUserPosts(id.Value,false);
                    if (arr == null) BadRequest("Something went wrong");
                }
                else
                {
                    arr = _repository.TweetDAO.GetUserPosts(userid, true);
                }
                return Ok(arr);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("userTweetsBatch")]
        [HttpPost]
        public IActionResult GetUserTweetsBatch([FromBody] ProfileState state)
        {
            IEnumerable<ReadableTweet> arr = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (state.id.HasValue)
                {
                    arr = _repository.TweetDAO.GetUserPostsBatch(state.id.Value,false, state.date);
                    if (arr == null) BadRequest("Something went wrong");
                }
                else
                {
                    arr = _repository.TweetDAO.GetUserPostsBatch(userid,true, state.date);
                }
                return Ok(arr);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }


        [Route("subscribe")]
        [HttpPost]
        public IActionResult Subscribe([FromBody] long id)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;
                _repository.UserDAO.Subscribe(userid,id,DateTime.Now);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong");
            }
        }
        
        [Route("unsubscribe")]
        [HttpPost]
        public IActionResult Unsubscribe([FromBody] long id)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;
                _repository.UserDAO.Unsubscribe(userid, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong");
            }
        }
        
        [Route("postTweet")]
        [HttpPost]
        public IActionResult PostTweet([FromBody] string message)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

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
        public IActionResult DeleteTweet([FromBody] long tweetId)
        {
            try
            {
                long userid = _tokenUserPairs.Pairs[Request.Cookies["Gruvo"]].Id;

                if(_repository.TweetDAO.CheckIfUserHasTweet(tweetId, userid))
                {
                    _repository.TweetDAO.DeletePost(tweetId);

                    return Ok("Success!");
                }
                else
                {
                    return BadRequest("Invalid tweet id!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

    }
}