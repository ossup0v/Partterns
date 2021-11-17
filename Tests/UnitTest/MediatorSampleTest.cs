using NUnit.Framework;
using Patterns.Main.Samples;

namespace UnitTest
{
    public class MediatorSampleTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TwoReceivers_TwoMediators_OneExecutor_TwoCommands_OneMessage()
        {
            var receiver = new MessageReceiver();
            var receiver2 = new MessageReceiver();
            var executor = new CommandExecutor();
            var mediator = new CommandMediator(receiver, executor);
            var mediator2 = new CommandMediator(receiver2, executor);

            var command1 = "C:ewqewqewq";
            
            receiver.ReceiveMessage(command1);
        
            var notCommand = "QQQQ";
            
            receiver.ReceiveMessage(notCommand);
            var command2 = "C:COMMAND2";
            
            receiver2.ReceiveMessage(command2);

            var allExecutedCommands = executor.GetAllExecutedCommands(); 
            Assert.AreEqual(allExecutedCommands.Count, 2);

            var indexer = 0;
            foreach (var receiverId in allExecutedCommands.Keys)
            {
                Assert.AreEqual(receiverId, indexer);
                indexer++;
            }
        }
    }
}