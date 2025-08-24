using LendingPlatform.Domain;

namespace LendingPlatform.Application;

public class LoanService
{
    private readonly List<LoanApplication> _applications = [];

    public LoanApplication ProcessApplication(decimal loanAmount, decimal assetValue, int creditScore)
    {
        var application = new LoanApplication(loanAmount, assetValue, new CreditScore(creditScore));

        application.EvaluateLoanApplication();

        _applications.Add(application);

        return application;
    }

    public int GetApplicantsCountByDecision(LoanDecision decision)
    {
        return _applications.Count(app => app.LoanDecision == decision);
    }

    public decimal GetTotalValueOfWrittenLoans()
    {
        return _applications
            .Where(app => app.LoanDecision == LoanDecision.Approved)
            .Sum(app => app.LoanAmount);
    }

    public decimal GetAverageLoanToValueRatio()
    {
        if (_applications.Count == 0)
        {
            return 0;
        }

        return _applications.Average(app => app.LoanToValueRatio);
    }
}

