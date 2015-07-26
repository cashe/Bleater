using System;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Service.Commands
{
    public class FollowCommand : ICommand
    {
        public string UserName { get; private set; }
        public string FollowedUserName { get; private set; }
        public DateTime DateCreated { get; private set; }

        public FollowCommand(string userName, string followedUserName, DateTime? dateCreated = null)
        {
            UserName = userName;
            FollowedUserName = followedUserName;
            DateCreated = dateCreated ?? DateTime.Now;
        }
    }
}