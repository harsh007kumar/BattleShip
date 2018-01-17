using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleShip;

namespace BattleShipUnitTest
{
    [TestClass]
    public class MyUnitTest
    {

        [TestMethod]
        [TestCategory("To Test Ship Class Object")]
        public void Test_Ship()
        {
            Ship testShip1 = new Ship("Q");
            Assert.AreEqual(false, testShip1.IsDestroyed);
            Assert.AreEqual(1, testShip1.ShipHeight);
            Assert.AreEqual(1, testShip1.ShipWidth);
            Assert.AreEqual(ShipType.Q, testShip1.TypeOfShip);
            Ship testShip2 = new Ship("P", "4", "2");
            Assert.AreEqual(false, testShip2.IsDestroyed);
            Assert.AreEqual(2, testShip2.ShipHeight);
            Assert.AreEqual(4, testShip2.ShipWidth);
            Assert.AreEqual(ShipType.P, testShip2.TypeOfShip);
        }

        [TestMethod]
        [TestCategory("To Test Player Class Object")]
        public void Test_Player()
        {
            // Creating a player class instance with 3 ships.
            Player testPlayer = new Player(3);
            testPlayer.AddShips("Q 2 3 A1 B2");
            testPlayer.AddMissileLaunchSequence("A1 B2 B3 A1 C1 E1 D4 D2 D7 E1");
            Assert.AreEqual("Player-1", testPlayer.Name);
            Assert.AreEqual(3, testPlayer.TotalNoOfShips);
            Assert.AreEqual(ShipType.Q, testPlayer.ListOfShipAndPosition[1].TypeOfShip);
            Assert.AreEqual(3, testPlayer.ListOfShipAndPosition[1].ShipHeight);
            Assert.AreEqual(2, testPlayer.ListOfShipAndPosition[1].ShipWidth);
            Assert.AreEqual("A1", testPlayer.FiringSequence.Dequeue());
        }

        [TestMethod]
        [TestCategory("To Test BattleArea Class Object")]
        public void Test_BattleArea()
        {
            int Player2;
            BattleArea testArea = new BattleArea(6, 'F');
            testArea.UpdateBattleFieldCells("A3", new Ship("P"));
            testArea.UpdateBattleFieldCells("F5", new Ship("Q"));
            Assert.AreEqual(3, testArea.TotalNoOfHitsBattleAreaCanTake);    // P ship take 1 hit and Q one takes 2 hit to get destroyed
            Assert.AreEqual(true, testArea.MissileHitOrMiss("A3", "Player-1", out Player2));
            Assert.AreEqual(true, testArea.MissileHitOrMiss("F5", "Player-1", out Player2));
            Assert.AreEqual("Player-1", testArea.Name);
            Assert.AreEqual(1,testArea.TotalNoOfHitsBattleAreaCanTake);     // Reduced to 1 as 2 missile have succesfully hit the Area
        }
    }
}
