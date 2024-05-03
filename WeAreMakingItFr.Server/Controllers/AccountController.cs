using Microsoft.AspNetCore.Mvc;
using PleaseAPI.DAL;
using WeAreMakingItFr.Server.Models;

namespace WeAreMakingItFr.Server.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        [HttpPost]
        [Route("/users/create")]
        public IActionResult CreateAccount([FromBody] User userToCreate)
        {
            if (DAL.UserDAL.CheckUserAvailable(userToCreate))
            {
                DAL.UserDAL.CreateNewUser(userToCreate);
                return Ok("User created");
            }
            else
            {
                return Conflict("User already exists. Please choose a different username.");
            }
        }

        [HttpPost]
        [Route("/users/login")]
        public IActionResult LoginUser([FromBody] User userToLogin)
        {
            if (DAL.UserDAL.CheckUserLogin(userToLogin))
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
    }
}
