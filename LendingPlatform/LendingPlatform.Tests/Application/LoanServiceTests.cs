using LendingPlatform.Application;
using LendingPlatform.Domain;

namespace LendingPlatform.Tests.Application;

public class LoanServiceTests
{
    private readonly LoanService _sut;

    public LoanServiceTests()
    {
        _sut = new LoanService();
    }

    [Fact]
    public void ProcessApplication_ShouldReturnCorrectDecision()
    {
        // Arrange
        decimal loanAmount = 100_000m;
        decimal assetValue = 200_000m;
        int creditScore = 750;

        // Act
        var application = _sut.ProcessApplication(loanAmount, assetValue, creditScore);

        // Assert
        Assert.NotNull(application);
        Assert.Equal(LoanDecision.Approved, application.LoanDecision);
    }

    [Fact]
    public void GetApplicantsCountByDecision_ShouldReturnCorrectCount()
    {
        // Arrange
        _sut.ProcessApplication(150_000m, 200_000m, 850);
        _sut.ProcessApplication(1_000_000m, 1_100_000m, 800);

        // Act
        var approvedCount = _sut.GetApplicantsCountByDecision(LoanDecision.Approved);
        var declinedCount = _sut.GetApplicantsCountByDecision(LoanDecision.Declined);

        // Assert
        Assert.Equal(1, approvedCount);
        Assert.Equal(1, declinedCount);
    }

    [Fact]
    public void GetTotalValueOfLoans_ShouldReturnSumOfApprovedLoans()
    {
        // Arrange
        _sut.ProcessApplication(100_000m, 200_000m, 750);
        _sut.ProcessApplication(1_500_000m, 2_000_000m, 600);

        // Act
        var totalValue = _sut.GetTotalValueOfWrittenLoans();

        // Assert
        Assert.Equal(100_000m, totalValue);
    }

    [Fact]
    public void GetAverageLoanToValueRatio_ShouldReturnCorrectAverage()
    {
        // Arrange
        _sut.ProcessApplication(100_000m, 200_000m, 750);
        _sut.ProcessApplication(1_000_000m, 2_000_000m, 800);

        // Act
        var averageLTV = _sut.GetAverageLoanToValueRatio();

        // Assert
        Assert.Equal(0.5m, averageLTV);
    }
}
