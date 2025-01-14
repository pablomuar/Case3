Feature: Square Root Calculation
  This feature calculates the square root of a number and validates the result.

  Scenario Outline: Calculate the square root of a number
    Given a number to calculate square root <number>
    When I calculate the square root
    Then the calculated square root should be <result>

    Examples:
      | number | result         |
      | 4      | 2              |
      | 9      | 3              |
      | 16     | 4              |
      | 25     | 5              |
      | -4     | Exception      |
      | -16    | Exception      |
      | -25    | Exception      |
      | -28    | Exception      |
      | 8      | 2.82           |
      | 45     | 6.70           |
      | 0      | 0              |
