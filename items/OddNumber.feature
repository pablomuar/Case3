Feature: Square Root

Scenario Outline: Calculate the square root of a number
    Given a number <number>
    When I calculate the square root
    Then the result should be <result>
    Examples: 
    | number | result  |
    | 1      | 1.0     |
    | 4      | 2.0     |
    | 9      | 3.0     |
    | 16     | 4.0     |
    | 25     | 5.0     |
