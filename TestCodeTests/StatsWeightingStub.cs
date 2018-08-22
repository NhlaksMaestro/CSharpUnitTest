using System;
using TestCode;

namespace TestCodeTests
{
    public class StatsWeightingStub : IStatsWeighting
    {
        // Sample implementation. Please read the instructions supplied with the test for the implementation required.
        public double Apply(double playerWinPercentage, int playerNumberOfMatches)
        {
            if (playerNumberOfMatches < 100) throw new ArgumentException("Player has not played more the 100 matches.", "playerNumberOfMatches");
            return 1.0;
        }
    }
}