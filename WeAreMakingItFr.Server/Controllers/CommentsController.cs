using Microsoft.AspNetCore.Mvc;
using PleaseAPI.DAL;
using PleaseAPI.Models;
using WeAreMakingItFr.Server.Models;

namespace WeAreMakingItFr.Server.Controllers
{
    [ApiController]
    public class CommentsController : Controller
    {

        [HttpGet]
        [Route("/comments/{messageId}")]
        public IActionResult GetCommentsByMessageId(int messageId)
        {
            //return Ok(DAL.CommentsDAL.getCommentsByMessageId(messageId));
            // vgm wordt dit niet gebruikt
            return Ok();
        }

        [HttpPost]
        [Route("/comments/{messageId}")]
        public IActionResult CreateComment([FromBody] Comment comment)
        {           
         
            DAL.CommentsDAL.CreateNewComment(comment);            

            return Ok("Comment created successfully");
        }
    }
}
