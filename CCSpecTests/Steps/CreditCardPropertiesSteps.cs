using CCSpecTests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecFlowTraining;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CCSpecTests.Steps
{
    [Binding]
    public class CreditCardPropertiesSteps: TechTalk.SpecFlow.Steps
    {
        //Commented as we are using this object from ScenarioContext class
        //private CreditCard _context.CC; 
        private readonly CCScenarioContext _context;
        public CreditCardPropertiesSteps(CCScenarioContext context)
        {
            _context = context;
        }

        //------Moved to CCCommonSteps class------
        //[Given(@"I have a credit card")]
        //public void GivenIHaveACreditCard()
        //{
        //    _context.CC = new CreditCard();
        //    //this.ScenarioContext["ccObj"] = _cc;
        //}

        [Given(@"the credit card limit is (.*) USD")]
        public void GivenTheCreditCardLimitIsUSD(double limit)
        {
            //======using implicit ScenarioContext dictionary for passinf CreditCard object======
            //var ccObj = (CreditCard)this.ScenarioContext["ccObj"];
            //ccObj.AddLimit(limit);
            _context.CC.AddLimit(limit);
        }

        //Scoped binding
        [Given(@"the credit card limit is (.*) USD")]
        [Scope(Tag ="scoped")]
        public void GivenTheCreditCardLimitIsUSD_New(double limit)
        {
            //======using implicit ScenarioContext dictionary for passinf CreditCard object======
            //var ccObj = (CreditCard)this.ScenarioContext["ccObj"];
            //ccObj.AddLimit(limit);
            _context.CC.AddLimit(limit);
            Console.WriteLine("This text is due to scoped binding");
        }

        [Then(@"credit card should be active")]
        public void ThenCreditCardShouldBeActive()
        {
            //======using implicit ScenarioContext dictionary for passinf CreditCard object======
            //var ccObj = (CreditCard)this.ScenarioContext["ccObj"];
            //Assert.IsTrue(ccObj.IsActive);
            Assert.IsTrue(_context.CC.IsActive);
        }

        [Then(@"the credit card balance should be equal to the limit")]
        public void ThenTheCreditCardBalanceShouldBeEqualToTheLimit()
        {
            Assert.AreEqual(_context.CC.Balance, _context.CC.Limit);
        }

        [Then(@"the credit card outstanding amount should be (.*)")]
        public void ThenTheCreditCardOutstandingAmountShouldBe(double outstanding)
        {
            Assert.AreEqual(outstanding, _context.CC.OutstandingAmount);
        }

        [Then(@"the credit card balance should be (.*)")]
        public void ThenTheCreditCardBalanceShouldBe(double balance)
        {
            Assert.AreEqual(balance, _context.CC.Balance);
        }

        [When(@"the card category is (.*)")]
        public void GivenTheCardCategoryIsGold(CardCategory category)
        {
            _context.CC.SetCategory(category);
        }

        [Then(@"total limit should be (.*)")]
        public void ThenTotalLimitShouldBe(double limit)
        {
            Assert.AreEqual(limit, _context.CC.Limit);
        }

        [When(@"the credit card is blocked")]
        public void WhenTheCreditCardIsBlocked()
        {
            _context.CC.Block();
        }

        [Then(@"the credit card's IsActive flag should be (.*)")]
        public void ThenTheCreditCardSIsActiveFlagShouldBeFalse(bool isActive)
        {
            Assert.AreEqual(isActive, _context.CC.IsActive);
        }

        [Given(@"extra offer on the limit is (.*)%")]
        public void GivenExtraOfferOnTheLimitIs(double percentage)
        {
            _context.CC.ApplyOffer(percentage);
        }

        [Given(@"limit and offer are as follows")]
        public void GivenLimitAndOfferAreAsFollows(Table table)
        {
            //==========Weakly typed conversion using linq==========
            //var limit = table.Rows.First(row => row["attribute"] == "limit")["value"];
            //var offer = table.Rows.First(row => row["attribute"] == "offer")["value"];

            //_context.CC.AddLimit(Convert.ToDouble(limit));
            //_context.CC.ApplyOffer(Convert.ToDouble(offer));

            //==========Strongly typed conversion based on CCLimitandOffer class==========
            //Using TechTalk.SpecFlow.Assist
            //var limitAndOffer = table.CreateInstance<CCLimitAndOffer>();
            //_context.CC.AddLimit(limitAndOffer.Limit);
            //_context.CC.ApplyOffer(limitAndOffer.Offer);


            //==========Dynamic Instance==========
            //Install SpecFlow.Assist.Dynamic nuget package
            dynamic attribute = table.CreateDynamicInstance();
            _context.CC.AddLimit(attribute.limit);
            _context.CC.ApplyOffer(attribute.offer);
        }

        [When(@"a transaction with below attributes is billed")]
        public void WhenATransactionWithBelowAttributesIsBilled(Table table)
        {
            //==========Weakly typed conversion==========
            //foreach(var row in table.Rows)
            //{
            //    double ItemPrice = Convert.ToDouble(row["ItemPrice"]);
            //    double cbPercentage = Convert.ToDouble(row["CashBackPercentage"]);
            //    double maxCb = Convert.ToDouble(row["MaxCashBack"]);
            //    Transaction tx = new Transaction(ItemPrice, cbPercentage, maxCb);
            //    _context.CC.BillTransaction(tx);
            //}

            //==========Strongly typed conversion based on CCLimitandOffer class==========
            //Using TechTalk.SpecFlow.Assist
            //IEnumerable<Transaction> txs = table.CreateSet<Transaction>();
            //foreach (var tx in txs)
            //    _context.CC.BillTransaction(tx);

            //==========Dynamic Set==========
            //Install SpecFlow.Assist.Dynamic nuget package
            IEnumerable<dynamic> transactions = table.CreateDynamicSet();
            foreach (var tx in transactions)
                _context.CC.BillTransaction(new Transaction(tx.ItemPrice, tx.CashBackPercentage, tx.MaxCashBack));
        }

        //Using Custom Conversion
        [When(@"the credit card due date has passed (.*)")]
        public void WhenTheCreditCardDueDateHasPassedDaysAgo(DateTime billDate)
        {
            _context.CC.SetBillDueDate(billDate);
        }

        [Then(@"credit card's status should be past due")]
        public void ThenCreditCardSStatusShouldBePastDue()
        {
            Assert.IsTrue(_context.CC.PastDue);
        }

        //Using auto custom conversion
        [When(@"a transaction with below attributes is billed - use auto custom conversion")]
        public void WhenATransactionWithBelowAttributesIsBilled_UseAutoCustomConversion(IEnumerable<Transaction> txObjects)
        {
            foreach (var tx in txObjects)
                _context.CC.BillTransaction(tx);
        }

    }
}
