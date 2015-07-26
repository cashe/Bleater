using System;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Service.Commands
{
    public class ReadCommand : ICommand
    {
        public string UserName { get; private set; }
        public DateTime DateCreated { get; private set; }

        public ReadCommand(string userName, DateTime? dateCreated = null)
        {
            UserName = userName;
            DateCreated = dateCreated ?? DateTime.Now;
        }
    }
}