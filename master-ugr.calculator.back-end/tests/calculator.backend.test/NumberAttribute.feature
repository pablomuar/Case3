Feature: Number Attribute
  I want to have a REST API which includes information
  about a number.

Scenario Outline: Checking several numbers
    When number <number> is checked for multiple attributes
    Then the answer to know whether is prime or not is <prime>
    And the answer to know whether is odd or not is <odd>
    And the square root of the number is <squareRoot>

Examples:
    | number | prime  | odd   | squareRoot   |
    | 2      | true   | false | 1.4142135624 |
    | 6      | false  | false | 2.4494897428 |
    | 7      | true   | true  | 2.6457513111 |
    | 8      | false  | false | 2.8284271247 |
    | 9      | false  | true  | 3            |
    | 10     | false  | false | 3.1622776602 |
