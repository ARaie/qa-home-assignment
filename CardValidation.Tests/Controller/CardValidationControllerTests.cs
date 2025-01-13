using CardValidation.ViewModels;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CardValidation.Tests
{
    public class CardValidationControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CardValidationControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();  // Use the factory to create a test client.
        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnOk_WhenCardIsValidVisa()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "John Doe",
                Number = "4111111111111111", // A valid Visa card number
                Date = "12/2025",
                Cvv = "123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            // Assert
            response.EnsureSuccessStatusCode();  // The response should be successful (200 OK).
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("10", responseBody);  // The response should contain "10" = "Visa", indicating the correct payment system.
        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnOk_WhenCardIsValidMasterCard()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "John Doe",
                Number = "5105105105105100", // A valid Master Card number
                Date = "12/2025",
                Cvv = "123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            // Assert
            response.EnsureSuccessStatusCode();  // The response should be successful (200 OK).
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("20", responseBody);  // The response should contain "20" = "MasterCard", indicating the correct payment system.
        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnOk_WhenCardIsValidAmericanExpress()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "John Doe",
                Number = "378282246310005", // A valid American Express card number
                Date = "12/2025",
                Cvv = "1234"
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            // Assert
            response.EnsureSuccessStatusCode();  // The response should be successful (200 OK).
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("30", responseBody);  // The response should contain "30" = "AmericanExpress", indicating the correct payment system.
        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnBadRequest_WhenCardIsInvalid()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "John Doe",
                Number = "1234567890123456", // Invalid card number
                Date = "12/2025",
                Cvv = "123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);
           
            response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest);  // 400 Bad Request expected

            var result = await response.Content.ReadAsStringAsync();

            result.Contains("Wrong number");
        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnBadRequest_WhenOwnerNameIsInvalid()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "", // Empty name
                Number = "4111111111111111", // A valid Visa card number
                Date = "12/2025",
                Cvv = "123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest);  // 400 Bad Request expected

            var result = await response.Content.ReadAsStringAsync();

            result.Contains("Owner is required");

        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnBadRequest_WhenDateIsInvalid()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "Jaagup Mets",
                Number = "4111111111111111", // A valid Visa card number
                Date = "1/2025", // Date must be > than today
                Cvv = "123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest);  // 400 Bad Request expected

            var result = await response.Content.ReadAsStringAsync();

            result.Contains("Wrong date");

        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnBadRequest_WhenCvvIsInvalid()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "Jaagup Mets",
                Number = "4111111111111111", // A valid Visa card number
                Date = "12/2025", 
                Cvv = "abc" // Must be numeric
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest);  // 400 Bad Request expected

            var result = await response.Content.ReadAsStringAsync();

            result.Contains("Wrong cvv");
        }

        [Fact]
        public async Task ValidateCreditCard_ShouldReturnBadRequest_WhenCvvIsEmpty()
        {
            // Arrange
            var creditCard = new CreditCard
            {
                Owner = "Jaagup Mets",
                Number = "4111111111111111", // A valid Visa card number
                Date = "12/2025", 
                Cvv = null
            };

            var content = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/CardValidation/card/credit/validate", content);

            response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest);  // 400 Bad Request expected

            var result = await response.Content.ReadAsStringAsync();

            result.Contains("Cvv is required");
        }

    }
}
