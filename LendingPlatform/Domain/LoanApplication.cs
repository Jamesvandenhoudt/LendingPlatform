namespace LendingPlatform.Domain;

/// <summary>
/// Represents a loan application.
/// </summary>
public class LoanApplication
{
    /// <summary>
    /// The loan amount requested by the applicant.
    /// </summary>
    public decimal LoanAmount { get; }

    /// <summary>
    /// The applicants asset value.
    /// </summary>
    public decimal AssetValue { get; }

    /// <summary>
    /// The applicant's credit score.
    /// </summary>
    public CreditScore CreditScore { get; }

    /// <summary>
    /// The decision made on the loan application.
    /// </summary>
    public LoanDecision LoanDecision { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoanApplication"/> class with the specified loan amount, asset value, and credit score.
    /// </summary>
    /// <param name="loanAmount"></param>
    /// <param name="assetValue"></param>
    /// <param name="creditScore"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public LoanApplication(decimal loanAmount, decimal assetValue, CreditScore creditScore)
    {
        if (loanAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(loanAmount), "Loan amount must be greater than zero.");
        }

        if (assetValue < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(assetValue), "Asset value cannot be negative.");
        }

        LoanAmount = loanAmount;
        AssetValue = assetValue;
        CreditScore = creditScore ?? throw new ArgumentNullException(nameof(creditScore), "Credit score cannot be null.");
        LoanDecision = LoanDecision.Pending;
    }

    /// <summary>
    /// The loan-to-value ratio calculated as the ratio of the loan amount to the asset value.
    /// </summary>
    public decimal LoanToValueRatio => CalculateLoanToValueRatio();

    /// <summary>
    /// Evaluates the loan application based on the loan amount, asset value, and credit score.
    /// </summary>
    public void EvaluateLoanApplication()
    {
        if (LoanAmount < 100000 || LoanAmount > 1500000)
        {
            LoanDecision = LoanDecision.Declined;
            return;
        }

        if (LoanAmount >= 1000000 && !(LoanToValueRatio <= 0.6m && CreditScore.Value >= 950))
        {
            LoanDecision = LoanDecision.Declined;
            return;
        }

        if (!MeetsCreditScore())
        {
            LoanDecision = LoanDecision.Declined;
            return;
        }

        LoanDecision = LoanDecision.Approved;
    }

    private bool MeetsCreditScore()
    {
        return LoanToValueRatio switch
        {
            < 0.6m => CreditScore.Value >= 750,
            < 0.8m => CreditScore.Value >= 800,
            < 0.9m => CreditScore.Value >= 900,
            _ => false
        };
    }

    private decimal CalculateLoanToValueRatio()
    {
        if (AssetValue == 0)
        {
            return 0;
        }

        return LoanAmount / AssetValue;
    }
}
