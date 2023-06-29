using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedeSocial.Entities
{
    public sealed class PostLike
    {
        private PostLike() { }

        public PostLike(Guid postId, Guid userId)
        {
            PostId = postId;
            UserId = userId;
        }

        [Key]
        public Guid PostId { get; private set; }
        [ForeignKey("PostId")]
        public Post? Post { get; private set; }

        [Key]
        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public User? User { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;
    }
}
