using System;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Service.Commands
{
    public class PostCommand : ICommand
    {
        public string UserName { get; private set; }
        public string Message { get; private set; }
        public DateTime DateCreated { get; private set; }

        public PostCommand(string userName, string message, DateTime? dateCreated = null)
        {
            
            UserName = userName;
            Message = message;
            DateCreated = dateCreated ?? DateTime.Now;
        }
    }
}