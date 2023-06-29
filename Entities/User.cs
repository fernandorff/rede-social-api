using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace RedeSocial.Entities
{
    public sealed class User
    {
        private User() { }

        public User(string firstName, string surname, string email, string nickname, DateTime dateOfBirth, string cep, string profilePictureUrl, string password)
        {
            UserId = Guid.NewGuid();
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Cep = cep;
            ProfilePictureUrl = profilePictureUrl;
            PasswordHash = GetMd5Hash(password);
        }

        [Key]
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Nickname { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public string Cep { get; private set; } = string.Empty;
        public string ProfilePictureUrl { get; private set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; private set; } = "user";

        public ICollection<Friendship> Friendships { get; private set; } = new List<Friendship>();

        private static string GetMd5Hash(string passwordInput)
        {
            using var md5Hash = MD5.Create();
            var data = Encoding.UTF8.GetBytes(passwordInput);
            var hashBytes = MD5.HashData(data);
            var sBuilder = new StringBuilder();

            foreach (var t in hashBytes)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public bool ValidatePassword(string password)
        {
            var hashedPassword = GetMd5Hash(password);
            return string.Equals(PasswordHash, hashedPassword);
        }

        public void Update(string nickname, string profilePictureUrl)
        {
            Nickname = nickname;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
