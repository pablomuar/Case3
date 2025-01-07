Feature: Number Attribute
 I want to have a REST API which includes information
 about a number.

 Scenario Outline: Checking several numbers
    When number <number> is checked for multiple attributes
    Then the answer to know whether is prime or not is <prime>
    And the answer to know whether is odd or not is <odd>
Examples:
    | number | prime  | odd   | sqrt  |
    | 2      | true   | false | 1.41  |
    | 6      | false  | false | 2.45  |
    | 7      | true   | true  | 2.65  |
    | 8      | false  | false | 2.83  |
    | 9      | false  | true  | 3.00  |
    | 10     | false  | false | 3.16  |
