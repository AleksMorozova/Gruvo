using System;
using Gruvo.DAL.Repository;
using Gruvo.DTL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy ="GruvoCookie")]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private BaseRepository _repository;

        public TweetController(BaseRepository repository)
        {
            _repository = repository;
        }

        [Route("like")]
        [HttpPost]
        public IActionResult Like([FromBody] LikeModel like)
        {
            try
            {
                if(like == null)
                {
                    return BadRequest();
                }

                if(_repository.TweetDAO.Dislike(like.PostId, like.UserId) == 0)
                {
                    _repository.TweetDAO.Like(like.PostId, like.UserId);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("tweetLikes")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetLikes()
        {
            return Ok();
        }
    }
}