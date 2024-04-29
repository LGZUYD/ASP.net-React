using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PleaseAPI.Models;
using System;
using Newtonsoft.Json;
using PleaseAPI;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace PleaseAPI.Controllers
{
    [ApiController]
    public class MessageController : Controller
    {
     
        [HttpGet]
        [Route("/messages")]
        public IActionResult GetMessages()
        {            
            // ...
            return Ok(DAL.DAL.MessageDAL.GetMessages());
        }

        [HttpGet]
        [Route("/messages/{id}")]
        public IActionResult GetMessageById(int id)
        {
            return Ok(id);
        }

        [HttpPost]
        [Route("/messages")]
        public IActionResult CreateMessage([FromBody] Message message)
        {
            //Message message = JsonConvert.DeserializeObject<Message>(requestBody.ToString());
            Console.WriteLine(message.MessageContent);
            DAL.DAL.MessageDAL.CreateNewPost(message);
            //this.GetMessages();

            return Ok("Message created successfully");
        }

        [HttpPut]
        [Route("/messages/{id}")]
        public IActionResult UpdateMessage([FromBody] Message message)
        {
            DAL.DAL.MessageDAL.UpdatePost(message);
            return Ok("Message deleted.");
        }

        [HttpDelete]
        [Route("/messages/{id}")]        
        public IActionResult DeleteMessage(int id)
        {
            DAL.DAL.MessageDAL.DeletePost(id);
            return Ok(id);
        }


    }
}
