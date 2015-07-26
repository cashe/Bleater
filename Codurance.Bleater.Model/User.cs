using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Codurance.Bleater.Model
{
    public class User
    {
        public string Name { get; set; }
        public ICollection<User> Following { get; private set; }
        public ICollection<Post> Posts { get; private set; }

        public User(string name)
        {
            Name = name;
            Following = new Collection<User>();
            Posts = new Collection<Post>();
        }

        public Post Post(string message, DateTime? dateCreated = null)
        {
            var post = new Post(this, message, dateCreated);
            Posts.Add(post);
            return post;
        }
    }
}
