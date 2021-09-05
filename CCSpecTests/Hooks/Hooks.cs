using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CCSpecTests.Hooks
{
    [Binding]
    public class Hooks: TechTalk.SpecFlow.Steps
    {
        [BeforeScenario]
        public void BeforeScenario1()
        {

        }

        [AfterScenario]
        public void AfterScenario()
        {

        }
    }
}
