using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gruvo.BLL;
using Gruvo.DAL.Repository;
using Gruvo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gruvo.Controllers
{
    [Authorize(Policy = "GruvoCookie")]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private BaseRepository _repository;
        private ITokenUserPairs _tokenUserPairs;
        private IHostingEnvironment _hostingEnvironment;

        public PhotoController(BaseRepository repository, ITokenUserPairs tokenUserPairs, IHostingEnvironment environment)
        {
            _repository = repository;
            _tokenUserPairs = tokenUserPairs;
            _hostingEnvironment = environment;
        }

        public class UploadImageModel
        {
            public IFormFile file { get; set; }
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> PhotoUpload(UploadImageModel model)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;

                var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "userPhotos");
                var file = model.file;
                _repository.UserDAO.UpdatePhoto(userid, file.FileName,0,0,0);
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }               

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Route("request/{id?}")]
        [HttpGet]
        public IActionResult RequestPhoto(long? id)
        {
            try
            {
                string cookie = Request.Cookies["Gruvo"];
                long userid = _tokenUserPairs.Pairs[cookie].Id;
                if (id.HasValue) userid = id.Value;
                string path = _repository.UserDAO.getPhoto(userid);
                byte[] dataBytes = System.IO.File.ReadAllBytes("userPhotos/" + path);
                var dataStream = new MemoryStream(dataBytes);

                return Ok(dataStream);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}