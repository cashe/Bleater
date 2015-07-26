using Codurance.Bleater.Model;

namespace Codurance.Bleater.Service.Interfaces
{
    public interface IUserRepository
    {
        User Get(string name);
    }
}