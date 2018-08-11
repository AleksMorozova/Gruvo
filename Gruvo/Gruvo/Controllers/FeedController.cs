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

        public FeedController(BaseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetTweets()
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = TokenUserPairs.GetInstance().GetPairs()[cookie].Id;
                return Ok(_repository.TweetDAO.GetPostsForUser(userid));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("recommendations")]
        public IActionResult GetRecommendations()
        {
            try
            {
                return Ok(_repository.UserDAO.GetRandomUsers(3));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}