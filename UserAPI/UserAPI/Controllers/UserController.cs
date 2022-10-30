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
    }
}
