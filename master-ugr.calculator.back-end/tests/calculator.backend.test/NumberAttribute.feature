Feature: Number Attribute
  As a user, I want to know the attributes of a number
  So that I can use them for mathematical analysis.

Scenario Outline: Checking attributes of valid numbers
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

Scenario: Checking attributes of zero
    When number 0 is checked for multiple attributes
    Then the answer to know whether is prime or not is false
    And the answer to know whether is odd or not is false
    And the square root of the number is 0

Scenario: Handling negative numbers
    When number -1 is checked for multiple attributes
    Then an error "Number must be non-negative" is returned

Scenario: Checking a very large number
    When number 100000000 is checked for multiple attributes
    Then the answer to know whether is prime or not is false
    And the answer to know whether is odd or not is false
    And the square root of the number is 10000
