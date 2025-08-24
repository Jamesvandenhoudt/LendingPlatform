namespace LendingPlatform.Domain;

/// <summary>
/// Represents a credit score.
/// </summary>
/// <param name="value"></param>
public class CreditScore
{
    /// <summary>
    /// The credit score value.
    /// </summary>
    public int Value { get; set; }

    public CreditScore(int value)
    {
        if (value < 1 || value > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Credit score must be between 1 and 999.");
        }

        Value = value;
    }
}
