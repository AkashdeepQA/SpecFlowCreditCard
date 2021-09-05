using System;

namespace SpecFlowTraining
{
    public class CreditCard
    {
        public CreditCard(double approvedLimit = 0)
        {
            ApprovedLimit = approvedLimit;
            Limit = approvedLimit;
            Balance = approvedLimit;
            OutstandingAmount = 0;
            IsActive = true;
            IsMaxedOut = false;
        }

        public double Balance { get; private set; }
        public bool IsActive { get; private set; }
        public double Limit { get; private set; }
        public double InterestRate { get; private set; }
        public double OutstandingAmount { get; private set; }
        public bool IsMaxedOut { get; private set; }
        public CardCategory Category { get; private set; }
        public double ApprovedLimit { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public bool PastDue { get; private set; }
        public DateTime BillDueDate { get; private set; }

        public void AddLimit(double limit)
        {
            ApprovedLimit = limit;
            Limit = limit;
            Balance = limit;
            OutstandingAmount = 0;
        }

        public void SetCategory(CardCategory category)
        {
            Category = category;
            double extraLimit;
            switch (category)
            {
                case CardCategory.Silver:
                    extraLimit = ApprovedLimit * 2.50 / 100;
                    Limit += extraLimit;
                    break;
                case CardCategory.Gold:
                    extraLimit = ApprovedLimit * 5 / 100;
                    Limit += extraLimit;
                    break;
                case CardCategory.Diamond:
                    extraLimit = ApprovedLimit * 7.5 / 100;
                    Limit += extraLimit;
                    break;
                case CardCategory.Platinum:
                    extraLimit = ApprovedLimit * 10 / 100;
                    Limit += extraLimit;
                    break;
                default:
                    break;
            }
        }

        public void ApplyOffer(double offerPercentage)
        {
            double extraLimit = ApprovedLimit * offerPercentage / 100;
            Limit += extraLimit;
        }

        public void Block()
        {
            IsActive = false;
        }

        public void BillTransaction(Transaction tx)
        {
            double txAmount;
            double cbAmount = 0;
            if (tx.CashBackPercentage != 0)
            {
                cbAmount = tx.ItemPrice * (tx.CashBackPercentage / 100);
                if (cbAmount >= tx.MaxCashBack)
                    cbAmount = tx.MaxCashBack;
            }
            txAmount = tx.ItemPrice - cbAmount;
            Balance -= txAmount;
            OutstandingAmount += txAmount;

            if (OutstandingAmount >= Limit)
                IsMaxedOut = true;
        }

        public void SetBillDueDate(DateTime date)
        {
            BillDueDate = date;
            if (DateTime.Today > date)
                PastDue = true;
        }
    }
}
