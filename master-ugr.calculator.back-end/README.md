## Mock Release Notes
## API Documentation

The `calculator.backend` project uses Swagger for API documentation. To view the API documentation, run the project and navigate to `/swagger/Index.html` in your browser.

## Running the Project
The project is deployed via CI/CD pipeline to an Azure App Service.
To run the project locally, follow these steps:
- Launch the project in Visual Studio
- Press `F5` to run the project
- Navigate to `https://localhost:44300/swagger/Index.html` in your browser
- Use the Swagger UI to interact with the API

## .net Project References
master-ugr.calculator.back-end.sln includes the following projects:
- calculator.backend: The main project that contains the API controllers and business logic
- calculator.backend.Tests: The unit test project that contains the unit tests for the API controllers and business logic
- calculator.lib: The library project that contains the business logic for the calculator

## Running the Unit Tests
The project uses Specfow & xUnit for unit testing. 
To run the unit test use following approach:
- Open two instances of Visual Studio on solution master-ugr.calculator.back-end.sln
- In first one, launch a debug session of calculator.backend project.
-- This will start the project.
- In second one, open Test Explorer and run all the tests.

## Supported Functionality
Following operations are offered as part of the back-end

Two numbers operation
- Add: Given two numbers, it returns the sum of the two numbers
- Substract: Given two numbers, it returns the difference of the two numbers
- Divide: Given two numbers, it returns the division of the two numbers
- Multiply: Given two numbers, it returns the multiplication of the two numbers

Also, attribute oprations are supported
- Prime: Given a number, it returns whether the number is prime or not.
- Odd: Given a number, it returns whether the number is odd or not.
# Functionality
- Previously, there was a mock for the odd numbers. Now it is behaving as expected.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

