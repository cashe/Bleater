using System;
using Codurance.Bleater.Service.Commands;
using Codurance.Bleater.Service.Interfaces;

namespace Codurance.Bleater.Service
{
    public class CommandParser
    {
        private const string PostIdentifier = " -> ";
        private const string FollowIdentifier = " follows ";
        private const string WallIdentifier = " wall";

        public ICommand Parse(string command, DateTime? dateCreated = null)
        {
            //Note that the POSTING check needs to come first otherwise we'd run into trouble with posts with the words 'follows' and 'wall' them
            
            //POSTING
            if (command.Contains(PostIdentifier))
            {
                var splits = command.Split(new[] { PostIdentifier }, StringSplitOptions.RemoveEmptyEntries);
                return new PostCommand(splits[0], splits[1], dateCreated);
            }

            //FOLLOWING
            if (command.Contains(FollowIdentifier))
            {
                var splits = command.Split(new[] { FollowIdentifier }, StringSplitOptions.RemoveEmptyEntries);
                return new FollowCommand(splits[0], splits[1], dateCreated);
            }

            //WALL
            if (command.Contains(WallIdentifier))
            {
                var userName = command.Replace(WallIdentifier, "");
                return new WallCommand(userName, dateCreated);
            }

            //READ
            return new ReadCommand(command, dateCreated);
        }
    }
}