using System.Collections.Generic;
using Codurance.Bleater.Model;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Service
{
    public class UserRepository : IUserRepository
    {
        //The key is the user's name
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();

        /// <summary>
        /// Creates a user if one doesn't already exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User Get(string name)
        {
            if (_users.ContainsKey(name))
            {
                return _users[name];
            }

            var user = new User(name);
            _users[name] = user;
            return user;
        }
    }
}
