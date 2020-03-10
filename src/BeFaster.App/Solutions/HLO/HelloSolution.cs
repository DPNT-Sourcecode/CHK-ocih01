using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.HLO
{
    public static class HelloSolution
    {
        public static string Hello(string friendName)
        {
            if(!string.IsNullOrEmpty(friendName))
                return $"Hello, {friendName}!";
            return "Hello";
        }
    }
}
