using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruvo.DAL.Repository;
using Gruvo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy = "GruvoCookie")]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private BaseRepository _repository;

        public SearchController(BaseRepository repository)
        {
            _repository = repository;
        }

        [Route("users")]
        [HttpGet]
        public IActionResult GetUsers([FromQuery] string login, [FromQuery] long? lastUserId)
        {
            IEnumerable<UserInfo> arr = null;
            try
            {
                arr = _repository.UserDAO.GetUsersByLogin(login, 5, lastUserId);

                return Ok(arr);

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}