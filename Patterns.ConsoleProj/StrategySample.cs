namespace Patterns.ConsoleProj
{
    public interface ICreditCartBillingStrategy
    {
        double GetCheck(int monthDuration);
    }
    
    public class PremiumCreditCartBillingStrategy : ICreditCartBillingStrategy
    {
        public double GetCheck(int monthDuration)
        {
            return 3.5 * monthDuration;
        }
    }
    
    public class CommonCreditCartBillingStrategy : ICreditCartBillingStrategy
    {
        public double GetCheck(int monthDuration)
        {
            return 5 * monthDuration;
        }
    }

    public class CreditCard
    {
        private readonly ICreditCartBillingStrategy _cartBilling;
        private int _montCounter;
        
        public CreditCard(ICreditCartBillingStrategy cartBilling)
        {
            _cartBilling = cartBilling;
        }

        public void SpentMonth() => _montCounter++;

        public double GetCheck() => _cartBilling.GetCheck(_montCounter);
    }
}