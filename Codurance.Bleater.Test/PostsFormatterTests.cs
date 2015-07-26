using System;
using System.Linq;
using Codurance.Bleater.Model;
using Codurance.Bleater.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codurance.Bleater.Test
{
    [TestClass]
    public class PostsFormatterTests
    {
        [TestMethod]
        public void Format_DisplayNamesTrue_IncludesUserNames()
        {
            //Arrange
            const string userName = "Chris";
            var user = new User(userName);
            var post = user.Post("Hello, world!");

            //Act
            var postsFormatter = new PostsFormatter();
            var results = postsFormatter.Format(true, new[] { post });

            //Assert
            Assert.IsTrue(results.First().Contains(userName), "User name should be in the output.");
        }

        [TestMethod]
        public void Format_DisplayNamesFalse_ExcludesUserNames()
        {
            //Arrange
            const string userName = "Chris";
            var user = new User(userName);
            var post = user.Post("Hello, world!");

            //Act
            var postsFormatter = new PostsFormatter();
            var results = postsFormatter.Format(false, new[] { post });

            //Assert
            Assert.IsFalse(results.First().Contains(userName), "User name should not be in the output.");
        }

        [TestMethod]
        public void Format_RelativeTime_OutputsCorrectTime()
        {
            //Arrange
            var now = DateTime.Now;
            var user = new User("Chris");
            var post = user.Post("Hello, world!", now - TimeSpan.FromSeconds(2));

            //Act
            var postsFormatter = new PostsFormatter();
            var results = postsFormatter.Format(false, new[] { post }, now);

            //Assert
            Assert.AreEqual("Hello, world! (2 seconds ago)", results.First());
        }

        [TestMethod]
        public void Format_MultiplePosts_NewestPostFirst()
        {
            //Arrange
            var now = DateTime.Now;
            var user = new User("Chris");
            var middlePost = user.Post("Middle", now - TimeSpan.FromSeconds(2));
            var newestPost = user.Post("Newest", now);
            var oldestPost = user.Post("Oldest", now - TimeSpan.FromSeconds(4));
            

            //Act
            var postsFormatter = new PostsFormatter();
            var results = postsFormatter.Format(false, new[] { middlePost, newestPost, oldestPost }, now);

            //Assert
            Assert.IsTrue(results.First().Contains("Newest"), "Expected newest post to be first");
        }
    }
}
