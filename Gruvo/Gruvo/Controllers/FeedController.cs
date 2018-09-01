using System;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy = "GruvoCookie")]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private BaseRepository _repository;
        private ITokenUserPairs _tokenUserPairs;

        public FeedController(BaseRepository repository, ITokenUserPairs tokenUserPairs)
        {
            _repository = repository;
            _tokenUserPairs = tokenUserPairs;
        }

        [HttpGet]
        [Route("tweets")]
        public IActionResult GetTweets()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;
                return Ok(_repository.TweetDAO.GetPostsForUser(userid));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("recommendations")]
        public IActionResult GetRecommendations()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;
                return Ok(_repository.UserDAO.GetRecommendations(userid, 3,100));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}