using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
  public  class CommandTests :IDisposable
    {

        Command testCommand;
        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something",
                Platform = "Some platform",
                CommandLine = "Some commandline"
            };
        }
        public void Dispose()
        {
            testCommand = null;
        }






        [Fact]
        public void CanChangeHowTo()
        {
            //Arrange
           
            //Act
            testCommand.HowTo = "Execute Unit Tests";
            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            //Arrange
           
            //Act
            testCommand.Platform = "platform";
            //Assert
            Assert.Equal("platform", testCommand.Platform);
        }


        [Fact]
        public void CanChangeCommandLine()
        {
            //Arrange
           
            //Act
            testCommand.CommandLine = "cmdline";
            //Assert
            Assert.Equal("cmdline", testCommand.CommandLine);
        }

       
    }
}
