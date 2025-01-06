Feature: Square Root

Scenario Outline: Calculate the square root of a number
    Given a number <number>
    When I calculate its square root
    Then the result should be <sqrt>
    Examples:
    | number | sqrt  |
    | 1      | 1     |
    | 2      | 1.41  |
    | 4      | 2     |
    | 9      | 3     |
    | 15     | 3.87  |
    | 16     | 4     |
