namespace PleaseAPI.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string? MessageContent{ get; set; }

        public Message()
        {            
        }

        public Message(int messageId, string messageContent)    
        {
            MessageId = messageId;
            MessageContent = messageContent;
        }
        public Message(string messageContent)
        {
            MessageContent = messageContent;
        }

    }
}
