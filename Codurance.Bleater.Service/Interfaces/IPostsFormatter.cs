using System;
using System.Collections.Generic;
using Codurance.Bleater.Model;

namespace Codurance.Bleater.Service.Interfaces
{
    public interface IPostsFormatter
    {
        IEnumerable<string> Format(bool displayUserNames, IEnumerable<Post> posts, DateTime? timeAgoRelativeTo = null);
    }
}