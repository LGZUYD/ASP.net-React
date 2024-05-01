namespace WeAreMakingItFr.Server.Models
{
    public class Comment
    {
        public int? CommentId { get; set; }
        public int MessageId { get; set; }
        public string? CommentContent { get; set; }

        public Comment()
        {
        }     

        public Comment(int commentId, int messageId, string commentContent)
        {
            CommentId = commentId;
            MessageId = messageId;
            CommentContent = commentContent;
        }

    }
}
