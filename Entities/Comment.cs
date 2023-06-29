using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedeSocial.Entities
{
    public sealed class Comment
    {
        private Comment() { }

        public Comment(string text, Guid postId, Guid userId)
        {
            CommentId = Guid.NewGuid();
            Text = text;
            PostId = postId;
            UserId = userId;
            CreatedAt = DateTime.Now;
            EditedAt = null;
        }

        [Key]
        public Guid CommentId { get; private set; }
        public string Text { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? EditedAt { get; private set; }


        public Guid PostId { get; private set; }
        [ForeignKey("PostId")]
        public Post? Post { get; private set; }

        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public User? User { get; private set; }

        public void Update(string text)
        {
            Text = text;
            EditedAt = DateTime.Now;
        }
    }
}
