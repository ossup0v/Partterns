using NUnit.Framework;
using Patterns.Main.Samples;

namespace UnitTest
{
    public class CommandTest
    {
        [Test]
        public void SmartHome_Commands_CheckForOpenWindow_CheckForTurnOnLight()
        {
            var manager = CommandManager.Instance;
            var home = new SmartHome(40, 3);
            var commandOne = new CheckForOpenWindowCommand(home);
            var commandTwo = new CheckForTurnOnLightCommand(home);
            
            manager.AddCommands(commandOne, commandTwo);
            manager.ExecuteAllCommands();
            
            Assert.AreEqual(home.Temperature, 35);
            Assert.AreEqual(home.LightValue, 8);
        }
    }
}