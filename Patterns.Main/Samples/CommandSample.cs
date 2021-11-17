using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Main.Samples
{
    public class SmartHome
    {
        public SmartHome(int temperature, int lightValue)
        {
            Temperature = temperature;
            LightValue = lightValue;
        }
        
        public int Temperature { get; private set; }
        public int LightValue { get; private set; }

        public void OpenWindow()
        {
            //To Work Here
            Temperature -= 5;
        }

        public void TurnOnLight()
        {
            //turning on light here
            LightValue += 5;
        }
    }

    public interface ISmartHomeCommand
    {
        SmartHomeResult Result { get; }
        void Execute();
    }

    public enum SmartHomeResult
    {
        Unexpected,
        NonCompleted,
        Completed,
        DoNonUsed
    }

    public class CheckForOpenWindowCommand : ISmartHomeCommand
    {
        private readonly SmartHome _home;

        public CheckForOpenWindowCommand(SmartHome home)
        {
            _home = home;
        }

        public SmartHomeResult Result { get; private set; } = SmartHomeResult.NonCompleted;
        
        public void Execute()
        {
            if (Result is SmartHomeResult.Completed or SmartHomeResult.DoNonUsed or SmartHomeResult.Unexpected)
            {
                Console.WriteLine($"Command {nameof(CheckForOpenWindowCommand)} is already completed! target {_home}.");
                return;
            }

            if (_home.Temperature > 25)
            {
                _home.OpenWindow();
                Result = SmartHomeResult.Completed;
                
                return;
            }

            Result = SmartHomeResult.DoNonUsed;
        }
    }

    public class CheckForTurnOnLightCommand : ISmartHomeCommand
    {
        private readonly SmartHome _home;

        public CheckForTurnOnLightCommand(SmartHome home)
        {
            _home = home;
        }

        public SmartHomeResult Result { get; private set; } = SmartHomeResult.NonCompleted;
        
        public void Execute()
        {
            if (Result is SmartHomeResult.Completed or SmartHomeResult.DoNonUsed or SmartHomeResult.Unexpected)
            {
                Console.WriteLine($"Command {nameof(CheckForTurnOnLightCommand)} is already completed! target {_home}.");
                return;
            }

            if (_home.LightValue < 10)
            {
                _home.TurnOnLight();
                Result = SmartHomeResult.Completed;
                return;
            }

            Result = SmartHomeResult.DoNonUsed;
        }
    }

    public class CommandManager
    {
        private CommandManager() { }
        public static CommandManager Instance { get; } = new();
        
        private readonly List<ISmartHomeCommand> _commands = new();

        public void AddCommands(params ISmartHomeCommand[] commands)
        {
            foreach (var command in commands)
                _commands.Add(command);
        }

        public void ExecuteAllCommands()
        {
            _commands.ForEach(command =>
            {
                switch (command.Result)
                {
                    case SmartHomeResult.NonCompleted: command.Execute(); break;;
                    //do logs here
                    case SmartHomeResult.Unexpected:
                    case SmartHomeResult.Completed:
                    case SmartHomeResult.DoNonUsed:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}