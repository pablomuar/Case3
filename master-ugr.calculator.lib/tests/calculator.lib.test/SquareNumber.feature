Feature: Square Root Calculation

Scenario Outline: Calculate the square root of a number
    Given a number to calculate square root <number>
    When I calculate the square root
    Then the result should be <result>

    Examples:
    | number | result |
    | 4      | 2      |
    | 9      | 3      |
    | 16     | 4      |
    | 25     | 5      |
    | -4     | null   |
