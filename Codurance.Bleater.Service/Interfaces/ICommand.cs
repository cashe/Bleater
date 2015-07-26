using System;

namespace Codurance.Bleater.Service.Interfaces
{
    public interface ICommand
    {
        DateTime DateCreated { get; }
    }
}