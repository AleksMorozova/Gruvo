using System;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy ="GruvoCookie")]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private BaseRepository _repository;
        private ITokenUserPairs _tokenUserPairs;

        public TweetController(BaseRepository repository, ITokenUserPairs tokenUserPairs)
        {
            _repository = repository;
            _tokenUserPairs = tokenUserPairs;
        }

        [Route("like")]
        [HttpPost]
        public IActionResult Like()
        {
            try
            {
                long tweetId = Convert.ToInt64(Request.Headers["tweetId"]);

                if (tweetId < 1)
                {
                    return BadRequest();
                }

                long userId = _tokenUserPairs.Pairs[Request.Cookies["Gruvo"]].Id;

                if (_repository.TweetDAO.Dislike(tweetId, userId) == 0)
                {
                    _repository.TweetDAO.Like(tweetId, userId);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("tweetLikes")]
        [HttpPost]
        [AllowAnonymous]// If we can see profile without logging in
        public IActionResult GetLikes()
        {
            try
            {
                long tweetId = Convert.ToInt64(Request.Headers["tweetId"]);

                if (tweetId < 1)
                {
                    return BadRequest();
                }

                return Ok(_repository.TweetDAO.GetNumOfLikes(tweetId));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}