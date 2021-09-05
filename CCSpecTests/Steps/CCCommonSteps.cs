using CCSpecTests.Context;
using SpecFlowTraining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CCSpecTests.Steps
{
    [Binding]
    public class CCCommonSteps
    {
        private CCScenarioContext _context;
        public CCCommonSteps(CCScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I have a credit card")]
        public void GivenIHaveACreditCard()
        {
            _context.CC = new CreditCard();
            //this.ScenarioContext["ccObj"] = _cc;
        }
    }
}
