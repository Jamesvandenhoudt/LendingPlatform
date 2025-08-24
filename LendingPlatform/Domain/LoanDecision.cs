namespace LendingPlatform.Domain;

/// <summary>
/// Enumeration representing the decision on a loan application.
/// </summary>
public enum LoanDecision
{
    /// <summary>
    /// The Approved status indicates that the loan application has been approved.
    /// </summary>
    Approved,

    /// <summary>
    /// The declined status indicates that the loan application has been declined.
    /// </summary>
    Declined,

    /// <summary>
    /// The Pending status indicates that the loan application has been submitted but not yet processed.
    /// </summary>
    Pending
}
