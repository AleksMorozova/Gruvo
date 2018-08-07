using System;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private BaseRepository _repository;

        public FeedController(BaseRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
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

    }
}