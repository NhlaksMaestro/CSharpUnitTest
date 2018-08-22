using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TestCode;
using TestCode.Models;

namespace TestCodeTests
{
    [TestClass]
    public class StatsCalculatorTests
    {
        [TestMethod]
        public void TestThatPlayerByPlayerNumberIsNotNull()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var result = sut.PlayerByPlayerNumber(5);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestThatPlayerByPlayerNumberHasCorrectPlayerNumberValue()
        {
            var playerNumber = 5;
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var result = sut.PlayerByPlayerNumber(playerNumber);
            Assert.AreEqual(result.PlayerNumber, playerNumber);
        }
        [TestMethod]
        public void TestThatPlayerByPlayerNumberIsOfTypePlayer()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var result = sut.PlayerByPlayerNumber(5);
            Assert.IsInstanceOfType(result, typeof(Player));
        }
        [TestMethod]
        public void TestThatPlayerByPlayerNumberIsNull()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var result = sut.PlayerByPlayerNumber(100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestThatTeamWinPercentageReturnsListIfTeamIdIsNotSpecified()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var result = sut.TeamWinPercentage();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestThatTeamWinPercentageReturnsListWithSpecifiedTeamIfTeamIdIsSpecified()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var resultWithTeamId = sut.TeamWinPercentage(2);
            var resultWithoutTeamId = sut.TeamWinPercentage();
            Assert.AreNotEqual(resultWithTeamId, resultWithoutTeamId);
        }

        [TestMethod]
        public void TestThatTeamWinPercentageReturnsEmptyListWithSpecifiedTeamIdIfTeamDoesNotExist()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var emptyListReturned = sut.TeamWinPercentage(7).ToList();
            Assert.IsTrue(emptyListReturned.Count == 0);
        }

        [TestMethod]
        public void TestThatTeamWinPercentageReturnsTeamsOrderedByPercantageDescending()
        {
            var sut = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
            var returned = sut.TeamWinPercentage().ToList();
            Assert.IsTrue(returned[0].TeamWinsPercentage > returned[1].TeamWinsPercentage);
        }
    }
}
