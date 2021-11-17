using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Main.Samples
{
    public class CommandMediator
    {
        private readonly MessageReceiver _receiver;
        private readonly CommandExecutor _executor;

        public CommandMediator(MessageReceiver receiver, CommandExecutor executor)
        {
            _receiver = receiver;
            _executor = executor;
            _receiver.OnMessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(string message)
        {
            if (message.StartsWith("C:"))
            {
                var pureCommand = message.Remove(0, 2);
                _executor.ExecuteCommand(pureCommand, _receiver.Id);
            }
        }
    }

    public class MessageReceiver
    {
        public event Action<string> OnMessageReceived = new (s => { });
        
        public readonly int Id = NewId;
        private static int _previousId;
        private static readonly object Locker = new();
        private static int NewId
        {
            get
            {
                lock (Locker)
                {
                    return _previousId++;
                }
            }
        }

        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"Message received: {message}");
                
            OnMessageReceived(message);
        }
    }

    public class CommandExecutor
    {
        private readonly Dictionary<int, List<string>> _executedCommands = new();

        public void ExecuteCommand(string command, int from)
        {
            Console.WriteLine($"Execute command from {from}");
            
            Console.WriteLine($"Making great job on command {command}");
            
            AddExecutedCommand(command, from);
        }

        public IReadOnlyDictionary<int, IReadOnlyList<string>> GetAllExecutedCommands()
        {
            //todo (osipov): make it better 
            return _executedCommands
                .ToDictionary(kvp => kvp.Key
                    , kvp => (IReadOnlyList<string>)kvp.Value);
        }

        private void AddExecutedCommand(string command, int from)
        {
            //todo (osipov): test performance            
            _executedCommands.TryGetValue(from, out var list);
            list ??= new List<string>();
            list.Add(command);
            _executedCommands.TryAdd(from, list);
        }
    }
}