using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {    
        public static int ComputePrice(string skus)
        {
            const int invalidInput = -1;

            if (string.IsNullOrWhiteSpace(skus)) { return invalidInput; }

            return 0;
        }
    }
}
