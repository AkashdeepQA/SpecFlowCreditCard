using SpecFlowTraining;
using TechTalk.SpecFlow;

namespace CCSpecTests.Context
{
    //Scenario context class
    //The object of this class will be used throughout the Steps definition classes
    //to pass on the context data between the step definitions
    [Binding]
    public class CCScenarioContext
    {
        public CreditCard CC { get; set; }
    }
}
