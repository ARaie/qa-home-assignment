# Home Assignment

Written unit tests and automated tests for a payment application. 

# Application information 

It’s an small microservice that validates provided Credit Card data and returns either an error or type of credit card application. 

# Project  

This repository contains the following projects:
1.	CardValidation.Core: Contains the logic for card validation services.
2.	CardValidation.Web: Contains the API controllers and configuration for the web application.
3.	CardValidation.Tests: Contains unit and integration tests for the API and services.

# Running Tests

## Run tests in Visual Studio:

 - dotnet clean
 - dotnet restore
 - dotnet build
 - dotnet test

 ## Run Tests in Docker container:

 - docker build -t qa-home-assignement .
 - docker run -d -p 8080:80 qa-home-assignement
 - docker exec $(docker ps -q --filter "ancestor=qa-home-assignement") bash -c "dotnet test /app/CardValidation.Tests/ --logger 'trx;LogFileName=/app/test-results.trx'"

## Testing in GitHub Actions:

 - The repository includes a GitHub Actions CI pipeline that runs the tests and builds the Docker container automatically on each push.

# Running the  application 

1. Clone the repository.
2. Compile and Run application Visual Studio 2022. 
3. Verify unit and integration tests in Visual Studio, Docker or in GitHub Actions CI pipeline.
