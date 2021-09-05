Feature: Credit Card Properties
As a new credit card holder
I want my credit card attributes to be correctly represented

Background:
	Given I have a credit card

#Data derived iterative Scenario
Scenario Outline: Combined Scenario
	Given the credit card limit is <limit> USD
	Then credit card should be active
	And the credit card balance should be <balance>
	And the credit card outstanding amount should be <outstanding>

	Examples:
		| limit  | balance | outstanding |
		| 50000  | 50000   | 0           |
		| 70000  | 70000   | 0           |
		| 100000 | 100000  | 0           |
		| 150000 | 150000  | 0           |
		| 200000 | 200000  | 0           |

#Scoped scenario to execute Same step "the credit card limit is 50000 USD" but with different implementation
@scoped
Scenario: Combined Scenario - ScenarioContextDemo
	Given the credit card limit is 50000 USD
	Then credit card should be active
	And the credit card balance should be 50000
	And the credit card outstanding amount should be 0

#String to Enum implicit conversion
@conversion
Scenario: Extra 10% limit on Gold category
	Given the credit card limit is 50000 USD
	When the card category is Gold
	Then total limit should be 52500

#String to bool implicit conversion
@conversion
Scenario: Card is blocked
	Given the credit card limit is 50000 USD
	When the credit card is blocked
	Then the credit card's IsActive flag should be false

Scenario: Extra 10% limit offer
	Given the credit card limit is 50000 USD
	And extra offer on the limit is 10%
	Then total limit should be 55000

#Step data table conversion
#Weakly typed using Linq
#Strongly typed using CreateInstance<T>()
#Strongly typed using CreateDynamicInstance()
@conversion
Scenario: Extra 10% limit offer using attributes
	Given limit and offer are as follows
		| attribute | value |
		| limit     | 50000 |
		| offer     | 10    |
	Then total limit should be 55000

#Multi-column step data table conversion
#Weakly typed using ForEach loop
#Strongly typed using CreateSet<T>()
#Strongly typed using CreateDynamicSet()
@conversion
Scenario: Bill multiple transactions with cashback
	Given the credit card limit is 50000 USD
	When a transaction with below attributes is billed
		| ItemPrice | CashBackPercentage | MaxCashBack |
		| 200       | 10                 | 15          |
		| 300       | 20                 | 50          |
		| 400       | 25                 | 70          |
	Then the credit card outstanding amount should be 765

#185+250+330=765
#Custom Transformation
#Custom conversion
#"3 days ago" to DateTime conversion
@conversion
Scenario: Card is pastDue if bill date is passed
	Given the credit card limit is 50000 USD
	When the credit card due date has passed 3 days ago
	Then credit card's status should be past due

#Automatic Custom Conversion based on the parameter type of step data definition
#Multi column step data table to IEnumerable<Transaction> set
@conversion
Scenario: Bill multiple transactions with cashback using uto custom conversion
	Given the credit card limit is 50000 USD
	When a transaction with below attributes is billed - use auto custom conversion
		| ItemPrice | CashBackPercentage | MaxCashBack |
		| 200       | 10                 | 15          |
		| 300       | 20                 | 50          |
		| 400       | 25                 | 70          |
	Then the credit card outstanding amount should be 765