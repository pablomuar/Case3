Feature: Odd Number

Scenario Outline: A number is odd if it is not divisible by 2
	Given a number <number>
	When I check if it is odd
	Then it should be odd <result>
	Examples: 
	| number | result |
	| 1      | true   |
	| 3      | true   |