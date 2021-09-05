using SpecFlowTraining;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CCSpecTests.Drivers
{
    //Class to perform custom conversion between the Step and the Step definition
    [Binding]
    class CustomConversion
    {
        [StepArgumentTransformation("(.*) days ago")]
        public DateTime DaysAgoConversion(int daysAgo)
        {
            return DateTime.Today.Subtract(TimeSpan.FromDays(daysAgo));
        }

        [StepArgumentTransformation]
        public IEnumerable<Transaction> TransactionObjectConversion(Table table)
        {
            return table.CreateSet<Transaction>();
        }
    }
}
