using NUnit.Framework;
using Patterns.ConsoleProj;

namespace UnitTest
{
    public class MonoStateTest
    {
        [Test]
        public void MonoState_TwoInstances()
        {
            var instanceOne = new MonoState();
            var instanceTwo = new MonoState();
            instanceOne.MonoStateInt = 1;
            Assert.AreEqual(instanceOne.MonoStateInt, instanceTwo.MonoStateInt);
            instanceTwo.MonoStateInt = 132;
            Assert.AreEqual(instanceOne.MonoStateInt, instanceTwo.MonoStateInt);
        }
    }
}