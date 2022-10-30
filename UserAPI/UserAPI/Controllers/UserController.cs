using Microsoft.AspNetCore.Mvc;
using UserAPI.DB;
using UserAPI.Model;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;

        public UserController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult AddUserInfo(UserInfo userInfo)
        {
            if(userInfo != null)
            {
                db.Users.Add(userInfo);
                db.SaveChanges();
            }
            return Ok(userInfo);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetUserById(long id)
        {
            var user = db.Users.Find(id);
            var viewUser = new ViewUserModel();

            if (user != null)
            {
                viewUser.Id = user.Id;
                viewUser.Name = user.FirstName + " " + user.LastName;
                viewUser.Phone = user.Phone;

                return Ok(viewUser);
            }

            else return BadRequest("Invalid ID");
        }


        [HttpPost]
        [Route("{id}/tags")]
        public IActionResult TagPost([FromRoute] long id, [FromBody] TagViewModel tagViewModel)
        {
            var user = db.Users.Find(id)
;
            if (user != null)
            {
                foreach (var tag in tagViewModel.Tags)
                {
                    var tempTag = new Tag();
                    tempTag.TagName = tag;
                    tempTag.UserId = user.Id;
                    tempTag.CreatedDate = DateTime.UtcNow;
                    tempTag.Expiry = tagViewModel.Expiry;

                    db.Tag.Add(tempTag);
                    db.SaveChanges();
                }

                return Ok("Task Added Successful");
            }
            else return BadRequest("Invalid ID");
        }

        
        
        [HttpGet]
        public IActionResult TagPost([FromQuery] List<string> tags)
        {
            var timenow = DateTime.UtcNow;
            var allTag = db.Tag.ToList();
            var filteredTags = new List<Tag>();



            if (allTag != null)
            {
                foreach (var item in allTag)
                {
                    if (tags.Contains(item.TagName) && (timenow - item.CreatedDate).TotalMilliseconds <= item.Expiry)
                    {
                        filteredTags.Add(item);
                    }
                }
            }

            var result = new List<UserTagViewModel>();
            var userIds = filteredTags.Select(x => x.UserId).Distinct().ToList();

            

            return Ok(result);
        }
    }
}
