using System;

namespace Codurance.Bleater.Model
{
    public class Post
    {
        public User User { get; private set; }
        public string Message { get; private set; }
        public DateTime DateCreated { get; private set; }

        public Post(User user, string message, DateTime? dateCreated = null)
        {
            User = user;
            Message = message;
            DateCreated = dateCreated ?? DateTime.Now;
        }
    }
}