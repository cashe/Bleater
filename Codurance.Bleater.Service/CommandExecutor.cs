using System;
using System.Collections.Generic;
using System.Linq;
using Codurance.Bleater.Service.Commands;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Service
{
    public class CommandExecutor
    {
        private readonly Action<IEnumerable<string>> _output;
        private readonly IUserRepository _userRepository;
        private readonly IPostsFormatter _postsFormatter;

        public CommandExecutor(IUserRepository userRepository, IPostsFormatter postsFormatter, Action<IEnumerable<string>> output)
        {
            _output = output;
            _userRepository = userRepository;
            _postsFormatter = postsFormatter;
        }

        public void Execute(ICommand command, DateTime? executionDate = null)
        {
            var followCommand = command as FollowCommand;
            if (followCommand != null)
            {
                var user = _userRepository.Get(followCommand.UserName);
                var followedUser = _userRepository.Get(followCommand.FollowedUserName);
                user.Following.Add(followedUser);
                return;
            }

            var postCommand = command as PostCommand;
            if (postCommand != null)
            {
                var user = _userRepository.Get(postCommand.UserName);
                user.Post(postCommand.Message, postCommand.DateCreated);
                return;
            }

            var readCommand = command as ReadCommand;
            if (readCommand != null)
            {
                var user = _userRepository.Get(readCommand.UserName);
                var formattedPosts = _postsFormatter.Format(false, user.Posts, executionDate);
                _output(formattedPosts);
                return;
            }

            var wallCommand = command as WallCommand;
            if (wallCommand != null)
            {
                var user = _userRepository.Get(wallCommand.UserName);

                //The wall combines a user's posts as well as the posts of everyone they follow
                var followingPosts = user.Following.SelectMany(u => u.Posts);
                var allPosts = user.Posts.Concat(followingPosts);
                var formattedPosts = _postsFormatter.Format(true, allPosts, executionDate);
                _output(formattedPosts);
                return;
            }

            throw new Exception("Unrecognized command type: " + command.GetType().FullName);
        }
    }
}