Feature: Square Root

Scenario Outline: Calculate the square root of a number
    Given a square-root number <number>
    When I calculate the square root of the number
    Then the square root result should be <sqrt>
    Examples:
    | number | sqrt  |
    | 1      | 1     |
    | 2      | 1.41  |
    | 4      | 2     |
    | 9      | 3     |
    | 15     | 3.87  |
    | 16     | 4     |
