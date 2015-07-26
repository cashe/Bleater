using System;
using System.Collections.Generic;
using System.Linq;
using Codurance.Bleater.Model;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Test
{
    public class PostsFormatterMock : IPostsFormatter
    {
        /// <summary>
        /// Returns the messages and nothing else
        /// </summary>
        public IEnumerable<string> Format(bool displayUserNames, IEnumerable<Post> posts, DateTime? timeAgoRelativeTo = null)
        {
            return posts.Select(p => p.Message);
        }
    }
}