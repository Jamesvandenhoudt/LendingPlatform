using LendingPlatform.Domain;

namespace LendingPlatform.Tests;

public class CreditScoreTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(300)]
    [InlineData(999)]
    public void ValidCreditScore_ShouldNotThrowException(int value)
    {
        // Arrange & Act
        var creditScore = new CreditScore(value);

        // Assert
        Assert.Equal(value, creditScore.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1000)]
    [InlineData(-1)]
    public void InvalidCreditScore_ShouldThrowArgumentOutOfRangeException(int value)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new CreditScore(value));
        Assert.Equal("value", exception.ParamName);
        Assert.StartsWith("Credit score must be between 1 and 999.", exception.Message);
    }

}

