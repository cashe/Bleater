using Codurance.Bleater.Service;
using Codurance.Bleater.Service.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codurance.Bleater.Test
{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void Parse_FollowsCommand_ReturnsPopulatedFollowCommand()
        {
            //Act
            var command = Parse<FollowCommand>("Chris follows Sophie");

            //Assert
            Assert.AreEqual("Chris", command.UserName, "Incorrect following user name.");
            Assert.AreEqual("Sophie", command.FollowedUserName, "Incorrect followed user name.");
        }

        [TestMethod]
        public void Parse_PostCommand_ReturnsPopulatedPostCommand()
        {
            //Act
            var command = Parse<PostCommand>("Chris -> Hello, world!");

            //Assert
            Assert.AreEqual("Chris", command.UserName, "Incorrect user name.");
            Assert.AreEqual("Hello, world!", command.Message, "Incorrect message.");
        }

        [TestMethod]
        public void Parse_ReadCommand_ReturnsPopulatedReadCommand()
        {
            //Act
            var command = Parse<ReadCommand>("Chris");

            //Assert
            Assert.AreEqual("Chris", command.UserName, "Incorrect user name.");
        }

        [TestMethod]
        public void Parse_WallCommand_ReturnsPopulatedWallCommand()
        {
            //Act
            var command = Parse<WallCommand>("Chris wall");

            //Assert
            Assert.AreEqual("Chris", command.UserName, "Incorrect user name.");
        }

        private T Parse<T>(string rawCommand)
        {
            var command = new CommandParser().Parse(rawCommand);
            Assert.IsInstanceOfType(command, typeof(T));
            return (T)command;
        }
    }
}
