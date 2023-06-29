using RedeSocial.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedeSocial.Entities
{
    public sealed class Post
    {
        private Post() { }

        public Post(string title, string text, string pictureUrl, PostVisibility visibility, Guid userId)
        {
            PostId = Guid.NewGuid();
            Title = title;
            Text = text;
            PictureUrl = pictureUrl;
            Visibility = visibility;
            UserId = userId;
            CreatedAt = DateTime.Now;
            EditedAt = null;
        }

        [Key]
        public Guid PostId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Text { get; private set; } = string.Empty;
        public string PictureUrl { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? EditedAt { get; private set; }
        public PostVisibility Visibility { get; private set; }

        public Guid UserId { get; private set; }
        [ForeignKey("UserId")]
        public User? User { get; private set; }

        public ICollection<PostLike> Likes { get; private set; } = new List<PostLike>();

        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        public void Update(string title, string text, string pictureUrl, PostVisibility visibility)
        {
            Title = title;
            Text = text;
            PictureUrl = pictureUrl;
            Visibility = visibility;
        }
    }
}
