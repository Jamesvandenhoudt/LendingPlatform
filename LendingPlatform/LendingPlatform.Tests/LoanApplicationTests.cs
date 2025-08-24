using LendingPlatform.Domain;

namespace LendingPlatform.Tests;

public class LoanApplicationTests
{
    [Fact]
    public void LoanBelowMinimumAmount_ShouldBeDeclined()
    {
        // Arrange
        var loanApplication = new LoanApplication(1000, 1000, new CreditScore(700));

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }

    [Fact]
    public void LoanAboveMaximumAmount_ShouldBeDeclined()
    {
        // Arrange
        var loanApplication = new LoanApplication(2_000_000, 3_000_000, new CreditScore(700));

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }

    [Fact]
    public void LoanOverOneMillion_LTVTooHigh_IsDeclined()
    {
        // Arrange
        var assetValue = 800_000m;
        var loanAmount = 1_000_000m;
        var creditScore = new CreditScore(900);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }

    [Fact]
    public void LoanOverOneMillion_CreditScoreTooLow_IsDeclined()
    {
        // Arrange
        var assetValue = 1_000_000m;
        var loanAmount = 200_000m;
        var creditScore = new CreditScore(600);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }    
    
    [Fact]
    public void LoanOverOneMillion_ValidLTVAndCreditScore_IsApproved()
    {
        // Arrange
        var assetValue = 1_000_000m;
        var loanAmount = 200_000m; 
        var creditScore = new CreditScore(950);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Approved, loanApplication.LoanDecision);
    }

    [Fact]
    public void LoanUnderOneMillion_ValidLTVAndCreditScore_IsApproved()
    {
        // Arrange
        var assetValue = 500_000m;
        var loanAmount = 200_000m;
        var creditScore = new CreditScore(750);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Approved, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_50Percent_CreditScore750_IsApproved()
    {
        // Arrange
        var assetValue = 200_000m;
        var loanAmount = 100_000m;
        var creditScore = new CreditScore(750);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Approved, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_50Percent_CreditScore700_IsDeclined()
    {
        // Arrange
        var assetValue = 200_000m;
        var loanAmount = 100_000m;
        var creditScore = new CreditScore(700);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_60Percent_CreditScore800_IsApproved()
    {
        // Arrange
        var assetValue = 250_000m;
        var loanAmount = 150_000m;
        var creditScore = new CreditScore(800);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Approved, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_60Percent_CreditScore700_IsDeclined()
    {
        // Arrange
        var assetValue = 250_000m;
        var loanAmount = 150_000m;
        var creditScore = new CreditScore(750);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_80Percent_CreditScore850_IsApproved()
    {
        // Arrange
        var assetValue = 500_000m;
        var loanAmount = 400_000m;
        var creditScore = new CreditScore(900);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Approved, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_80Percent_CreditScore800_IsDeclined()
    {
        // Arrange
        var assetValue = 500_000m;
        var loanAmount = 400_000m;
        var creditScore = new CreditScore(800);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }

    [Fact]
    public void LTV_90Percent_AutomaticallyDeclined()
    {
        // Arrange
        var assetValue = 1_000_000m;
        var loanAmount = 900_000m;
        var creditScore = new CreditScore(850);

        var loanApplication = new LoanApplication(loanAmount, assetValue, creditScore);

        // Act
        loanApplication.EvaluateLoanApplication();

        // Assert
        Assert.Equal(LoanDecision.Declined, loanApplication.LoanDecision);
    }
}
