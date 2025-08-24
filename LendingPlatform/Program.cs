using LendingPlatform.Application;
using LendingPlatform.Domain;

class Program
{
    static void Main()
    {
        var service = new LoanService();

        Console.WriteLine("Welcome to the Lending Platform!");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("Enter loan amount (or type 'q' to quit):");
            var loanInput = Console.ReadLine();
            if (loanInput?.ToLower() == "q")
            {
                break;
            }

            Console.WriteLine("Enter asset value:");
            var assetInput = Console.ReadLine();

            Console.WriteLine("Enter credit score:");
            var creditScoreInput = Console.ReadLine();

            Console.WriteLine();

            if (!decimal.TryParse(loanInput, out var loanAmount) ||
                !decimal.TryParse(assetInput, out var assetValue) ||
                !int.TryParse(creditScoreInput, out var creditScore))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            try
            {
                var application = service.ProcessApplication(loanAmount, assetValue, creditScore);

                Console.WriteLine($"Loan Decision: {application.LoanDecision}");

                Console.WriteLine($"Total Successful Applicants: {service.GetApplicantsCountByDecision(LoanDecision.Approved)}");
                Console.WriteLine($"Total Declined Applicants: {service.GetApplicantsCountByDecision(LoanDecision.Declined)}");

                Console.WriteLine($"Total Value of Written Loans: {service.GetTotalValueOfWrittenLoans():C}");
                Console.WriteLine($"Average Loan-to-Value Ratio: {service.GetAverageLoanToValueRatio():P2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing application: {ex.Message}");
            }

            Console.WriteLine();

        }

        Console.WriteLine("Thank you for using the Lending Platform!");
    }
}