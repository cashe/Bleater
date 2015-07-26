using System.Collections.Generic;
using System.Linq;
using Codurance.Bleater.Model;
using Codurance.Bleater.Service;
using Codurance.Bleater.Service.Commands;
using Codurance.Bleater.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Codurance.Bleater.Test
{
    [TestClass]
    public class CommandExecutorTests : TestBase
    {
        [TestMethod]
        public void Execute_FollowCommand_PopulatesUsersFollowing()
        {
            //Arrange - Create our users
            const string userName = "Chris";
            const string followedUserName = "Sophie";
            var user = new User(userName);
            var followedUser = new User(followedUserName);

            //Arrange - the user repo should return our users
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup((r => r.Get(userName))).Returns(user);
            mockRepository.Setup((r => r.Get(followedUserName))).Returns(followedUser);

            var commandExecutor = new CommandExecutor(mockRepository.Object, new Mock<IPostsFormatter>().Object, WriteLines);

            //Act
            commandExecutor.Execute(new FollowCommand(userName, followedUserName));

            //Assert
            Assert.IsTrue(user.Following.Contains(followedUser), "Expected {0} to be following {1}.", user, followedUser);
        }

        [TestMethod]
        public void Execute_PostCommand_PopulatesUsersPosts()
        {
            //Arrange - Create our user
            const string userName = "Chris";
            var user = new User(userName);

            //Arrange - the user repo should return our user
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup((r => r.Get(userName))).Returns(user);

            var commandExecutor = new CommandExecutor(mockRepository.Object, new Mock<IPostsFormatter>().Object, WriteLines);

            //Act
            const string message = "Hello, world!";
            commandExecutor.Execute(new PostCommand(userName, message));

            //Assert
            Assert.IsTrue(user.Posts.Any(p => p.Message == message), "Expected post with text '{0}'.", message);
        }


        [TestMethod]
        public void Execute_ReadCommand_ReturnsUsersPosts()
        {
            //Arrange - Create our user with 2 posts
            const string userName = "Chris";
            var user = new User(userName);
            const string firstPostMessage = "Hello, world!";
            const string secondPostMessage = "Goodbye, world!";
            user.Post(firstPostMessage);
            user.Post(secondPostMessage);

            //Arrange - the user repo should return our user
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup((r => r.Get(userName))).Returns(user);

            var output = Enumerable.Empty<string>();
            var commandExecutor = new CommandExecutor(mockRepository.Object, new PostsFormatterMock(), o => output = o);

            //Act
            commandExecutor.Execute(new ReadCommand(userName));

            //Assert
            Assert.IsTrue(output.Contains(firstPostMessage), "Expected post with text '{0}'.", firstPostMessage);
            Assert.IsTrue(output.Contains(secondPostMessage), "Expected post with text '{0}'.", secondPostMessage);
        }

        [TestMethod]
        public void Execute_WallCommand_ReturnsCurrentUsersAndFollowingUsersPosts()
        {
            //Arrange - Create user
            const string userName = "Chris";
            var user = new User(userName);
            const string userPostMessage = "I like Sophie";
            user.Post(userPostMessage);

            //Arrange - Created followed user
            const string followedUserName = "Sophie";
            var followedUser = new User(followedUserName);
            const string followedUserPostMessage = "I like Chris";
            followedUser.Post(followedUserPostMessage);

            //Arrange - Set up following
            user.Following.Add(followedUser);
            
            //Arrange - the user repo should return our users
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup((r => r.Get(userName))).Returns(user);
            mockRepository.Setup((r => r.Get(followedUserName))).Returns(followedUser);

            var output = Enumerable.Empty<string>();
            var commandExecutor = new CommandExecutor(mockRepository.Object, new PostsFormatterMock(), o => output = o);

            //Act
            commandExecutor.Execute(new WallCommand(userName));

            //Assert
            Assert.IsTrue(output.Contains(userPostMessage), "Expected reults to contain user's post, i.e. '{0}'.", userPostMessage);
            Assert.IsTrue(output.Contains(followedUserPostMessage), "Expected reults to contain followed user's post, i.e. '{0}'.", followedUserPostMessage);
        }

        private void WriteLines(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                TestContext.WriteLine(message);
            }
            
        }
    }
}
