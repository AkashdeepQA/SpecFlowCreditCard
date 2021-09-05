namespace SpecFlowTraining
{
    public class Transaction
    {
        public Transaction(double ItemPrice, double CashBackPercentage, double MaxCashBack)
        {
            this.ItemPrice = ItemPrice;
            this.CashBackPercentage = CashBackPercentage;
            this.MaxCashBack = MaxCashBack;
        }
        public double ItemPrice { get; private set; }
        public double CashBackPercentage { get; private set; }
        public double MaxCashBack { get; private set; }
    }

}
