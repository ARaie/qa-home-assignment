using CardValidation.Core.Services;
using CardValidation.Core.Services.Interfaces;
using CardValidation.Core.Enums;
using Xunit;

namespace CardValidation.Tests
{
    public class CardValidationServiceTests
    {
        private readonly CardValidationService _cardValidationService;

        public CardValidationServiceTests()
        {
            _cardValidationService = new CardValidationService();
        }

        [Fact]
        public void ValidateOwner_ShouldReturnTrue_WhenNameIsValid()
        {
            // Arrange
            var owner = "John Doe";  // A valid owner name.

            // Act
            var result = _cardValidationService.ValidateOwner(owner);

            // Assert
            Assert.True(result);  // The result should be true since the name is valid.
        }

        [Fact]
        public void ValidateOwner_ShouldReturnFalse_WhenNameIsInvalid()
        {
            // Arrange
            var owner = "John123";  // Invalid owner name with numbers.

            // Act
            var result = _cardValidationService.ValidateOwner(owner);

            // Assert
            Assert.False(result);  // The result should be false because the name is invalid.
        }

        [Fact]
        public void ValidateOwner_ShouldReturnFalse_WhenNameIsEmpty()
        {
            // Arrange
            var owner = "";  // Empty owner name.

            // Act
            var result = _cardValidationService.ValidateOwner(owner);

            // Assert
            Assert.False(result);  // The result should be false because the name is invalid.
        }

        [Fact]
        public void ValidateIssueDate_ShouldReturnTrue_WhenDateIsValid()
        {
            // Arrange
            var issueDate = "12/2025";  // Valid date format.

            // Act
            var result = _cardValidationService.ValidateIssueDate(issueDate);

            // Assert
            Assert.True(result);  // The result should be true since the date is valid.
        }

        [Fact]
        public void ValidateIssueDate_ShouldReturnFalse_WhenDateIsInvalid()
        {
            // Arrange
            var issueDate = "13/2025";  // Invalid date (month exceeds 12).

            // Act
            var result = _cardValidationService.ValidateIssueDate(issueDate);

            // Assert
            Assert.False(result);  // The result should be false due to invalid date.
        }

        [Fact]
        public void ValidateCvc_ShouldReturnTrue_WhenCvcIsValid()
        {
            // Arrange
            var cvc = "123";  // Valid CVC.

            // Act
            var result = _cardValidationService.ValidateCvc(cvc);

            // Assert
            Assert.True(result);  // The result should be true since the CVC is valid.
        }

        [Fact]
        public void ValidateCvc_ShouldReturnFalse_WhenCvcIsInvalid()
        {
            // Arrange
            var cvc = "12345";  // Invalid CVC (more than 4 digits).

            // Act
            var result = _cardValidationService.ValidateCvc(cvc);

            // Assert
            Assert.False(result);  // The result should be false due to invalid CVC.
        }

        [Fact]
        public void ValidateNumber_ShouldReturnTrue_WhenCardNumberIsValidVisa()
        {
            // Arrange
            var cardNumber = "4111111111111111";  // Valid Visa card number.

            // Act
            var result = _cardValidationService.ValidateNumber(cardNumber);

            // Assert
            Assert.True(result);  // The result should be true since the number is a valid Visa card number.
        }

        [Fact]
        public void ValidateNumber_ShouldReturnFalse_WhenCardNumberIsInvalid()
        {
            // Arrange
            var cardNumber = "1234567890123456";  // Invalid card number.

            // Act
            var result = _cardValidationService.ValidateNumber(cardNumber);

            // Assert
            Assert.False(result);  // The result should be false since the number is not a valid card number.
        }

        [Fact]
        public void GetPaymentSystemType_ShouldReturnVisa_WhenCardNumberIsVisa()
        {
            // Arrange
            var cardNumber = "4111111111111111";  // A valid Visa card number.

            // Act
            var result = _cardValidationService.GetPaymentSystemType(cardNumber);

            // Assert
            Assert.Equal(PaymentSystemType.Visa, result);  // The result should be Visa.
        }

        [Fact]
        public void GetPaymentSystemType_ShouldThrowException_WhenCardNumberIsInvalid()
        {
            // Arrange
            var cardNumber = "1234567890123456";  // Invalid card number.

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _cardValidationService.GetPaymentSystemType(cardNumber));
            // The method should throw NotImplementedException for an unknown card type.
        }
    }
}
