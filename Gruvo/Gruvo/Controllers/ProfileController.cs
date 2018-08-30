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
                    if (id.Value == userid) throw new ArgumentException();
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
        public IActionResult GetSubscribers(long? id)
        {
            IEnumerable<UserInfo> arr = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                if (id.HasValue)
                {
                    arr = _repository.UserDAO.GetSubscribers(id.Value);
                    if (arr == null) throw new NullReferenceException();
                }
                else
                {
                    arr = _repository.UserDAO.GetSubscribers(userid);
                }
                return Ok(arr);
            
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("subscriptions/{id?}")]
        [HttpGet]
        public IActionResult GetSubscriptions(long? id)
        {
            IEnumerable<UserInfo> arr = null;
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id; 

                if (id.HasValue)
                {
                    arr = _repository.UserDAO.GetSubscriptions(id.Value);
                    if (arr == null) throw new NullReferenceException();
                }
                else
                {
                    arr = _repository.UserDAO.GetSubscriptions(userid);
                }
                return Ok(arr);
            

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
                    arr = _repository.TweetDAO.GetUserPosts(id.Value);
                    if (arr == null) throw new NullReferenceException();
                }
                else
                {
                    arr = _repository.TweetDAO.GetUserPosts(userid);
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
        public IActionResult DeleteTweet()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

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