using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Codurance.Bleater.Model;
using Codurance.Bleater.Service.Interfaces;
using TimeAgo;

namespace Codurance.Bleater.Service
{
    public class PostsFormatter : IPostsFormatter
    {
        public IEnumerable<string> Format(bool displayUserNames, IEnumerable<Post> posts, DateTime? timeAgoRelativeTo = null)
        {
            if (!timeAgoRelativeTo.HasValue)
            {
                timeAgoRelativeTo = DateTime.Now;
            }

            return from p in posts
                   let userPrefix = displayUserNames ? p.User.Name + " - " : ""
                   let timeAgo = p.DateCreated.TimeAgo(timeAgoRelativeTo.Value, Thread.CurrentThread.CurrentCulture)
                   orderby p.DateCreated descending //We show the most recent posts first
                   select String.Format("{0}{1} ({2})", userPrefix, p.Message, timeAgo);
        }
    }
}