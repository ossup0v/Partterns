using NUnit.Framework;
using Patterns.Main.Samples;

namespace UnitTest
{
    public class StrategyTest
    {
        [Test]
        public void Two_Strategies_One_Env()
        {
            var prem = new PremiumCreditCartBillingStrategy();
            var common = new CommonCreditCartBillingStrategy();
            
            var premCard = new CreditCard(prem);
            var commonCard = new CreditCard(common);
            
            premCard.SpentMonth();
            commonCard.SpentMonth();
            
            Assert.AreNotEqual(premCard.GetCheck(), commonCard.GetCheck());
            Assert.AreEqual(premCard.GetCheck(), 3.5);
            Assert.AreEqual(commonCard.GetCheck(), 5);
        }
    }
}