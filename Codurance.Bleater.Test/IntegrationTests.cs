using System;
using System.Linq;
using Codurance.Bleater.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codurance.Bleater.Test
{
    [TestClass]
    public class IntegrationTests : TestBase
    {
        [TestMethod]
        public void Program_Matches_Spec()
        {
            //We freezing time so that code execution speed doesn't have an impact
            //on the timestamps in our output (eg 1 second ago, 5 minutes ago)
            var now = DateTime.Now;

            var userRepository = new UserRepository();
            var postsFormatter = new PostsFormatter();
            var output = Enumerable.Empty<string>();
            var commandExecutor = new CommandExecutor(userRepository, postsFormatter, o => output = o);
            var commandParser = new CommandParser();

            var alicePost1Command = commandParser.Parse("Alice -> I love the weather today", now - TimeSpan.FromMinutes(5));
            commandExecutor.Execute(alicePost1Command);

            var bobPost1Command = commandParser.Parse("Bob -> Damn! We lost!", now - TimeSpan.FromMinutes(2));
            commandExecutor.Execute(bobPost1Command);

            var bobPost2Command = commandParser.Parse("Bob -> Good game though.", now - TimeSpan.FromMinutes(1));
            commandExecutor.Execute(bobPost2Command);

            var readAliceCommand = commandParser.Parse("Alice", now);
            commandExecutor.Execute(readAliceCommand, now);
            Assert.AreEqual(1, output.ToArray().Length, "Expected 1 post");
            Assert.AreEqual("I love the weather today (5 minutes ago)", output.First());

            var readBobCommand = commandParser.Parse("Bob", now);
            commandExecutor.Execute(readBobCommand, now);
            Assert.AreEqual(2, output.ToArray().Length, "Expected 2 posts");
            Assert.AreEqual("Good game though. (1 minute ago)", output.First());
            Assert.AreEqual("Damn! We lost! (2 minutes ago)", output.ElementAt(1));

            var charliePostCommand = commandParser.Parse("Charlie -> I'm in New York today! Anyone want to have a coffee?", now - TimeSpan.FromSeconds(2));
            commandExecutor.Execute(charliePostCommand);

            var charlieFollowsAliceCommand = commandParser.Parse("Charlie follows Alice");
            commandExecutor.Execute(charlieFollowsAliceCommand);

            var charlieWallCommand = commandParser.Parse("Charlie wall", now);
            commandExecutor.Execute(charlieWallCommand, now);
            Assert.AreEqual(2, output.ToArray().Length, "Expected 2 posts");
            Assert.AreEqual("Charlie - I'm in New York today! Anyone want to have a coffee? (2 seconds ago)", output.First());
            Assert.AreEqual("Alice - I love the weather today (5 minutes ago)", output.ElementAt(1));

            var charlieFollowsBobCommand = commandParser.Parse("Charlie follows Bob");
            commandExecutor.Execute(charlieFollowsBobCommand);

            var charlieWall2Command = commandParser.Parse("Charlie wall", now);
            commandExecutor.Execute(charlieWall2Command, now);
            Assert.AreEqual(4, output.ToArray().Length, "Expected 4 posts");
            Assert.AreEqual("Charlie - I'm in New York today! Anyone want to have a coffee? (2 seconds ago)", output.First());
            Assert.AreEqual("Bob - Good game though. (1 minute ago)", output.ElementAt(1));
            Assert.AreEqual("Bob - Damn! We lost! (2 minutes ago)", output.ElementAt(2));
            Assert.AreEqual("Alice - I love the weather today (5 minutes ago)", output.ElementAt(3));
        }
    }
}
