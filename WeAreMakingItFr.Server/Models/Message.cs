using WeAreMakingItFr.Server.Models;

namespace PleaseAPI.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string? MessageContent{ get; set; }
        public List<Comment>? AllComments { get; set; }

        public int? UserId { get; set; }

        public Message()
        {            
        }
        public Message(int messageId, string messageContent, List<Comment> allComments/*, int userId*/)    
        {
            MessageId = messageId;
            MessageContent = messageContent;
            AllComments = allComments;
            //UserId = userId;    
        }
        public Message(string messageContent)
        {
            MessageContent = messageContent;
        }

    }
}
