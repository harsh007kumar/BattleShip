using System;
using System.Collections.Generic;
namespace BattleShip
{
    public class BattleArea
    {
        // Height of Battle area
        public int HeightOfBattleArea { get; set; }

        // Width of Battle area
        public int WidthOfBattleArea { get; set; }

        // No Of successfull hits Battle Arena can take [i.e, noOfPtype+(2*noofQtype)]
        int totalNoOfHitsBattleAreaCanTake = 0;

        public int TotalNoOfHitsBattleAreaCanTake { get { return totalNoOfHitsBattleAreaCanTake; } }

        static int PlayerNo = 1;

        string name;
        public string Name { get { return name; } }

        private Cell[,] Area;

        public BattleArea(int column, char rows)
        {
            this.name = "Player-" + PlayerNo;
            PlayerNo++;

            WidthOfBattleArea = column;
            HeightOfBattleArea = Helper.AlphabetToNo(rows);
            if ((WidthOfBattleArea >= 1 && WidthOfBattleArea <= 9) && (HeightOfBattleArea >= 1 && HeightOfBattleArea <= 26))
            {
                Area = new Cell[HeightOfBattleArea, WidthOfBattleArea];
                PrepareBattleField();
            }
            else
            {
                // Raise Error - "Dimensions for BatteField Exceeds Predefined Max Limit"
                throw new Exception("Dimensions for BatteField Exceeds Predefined Max Limit");
            }
        }

        // Create Empty BattleField as per the given width & height
        public void PrepareBattleField()
        {
            for (int x = 0; x < HeightOfBattleArea; x++)
            {
                for (int y = 0; y < WidthOfBattleArea; y++)
                { Area[x, y] = new Cell(); }
            }
        }


        // Updating cells of battlefield as per size of individual ship
        public void UpdateBattleFieldCells(string position, Ship ship)
        {
            int startingRow = Helper.AlphabetToNo(position) - 1;
            int startingColumn = Convert.ToInt32(position.Substring(1, 1)) - 1;
            int shipWidth, shipHeight;

            for (shipWidth = startingColumn; shipWidth < ship.ShipWidth + startingColumn; shipWidth++)
            {
                for (shipHeight = startingRow; shipHeight < ship.ShipHeight + startingRow; shipHeight++)
                {
                    Area[shipHeight, shipWidth] = new Cell((int)ship.TypeOfShip);
                    totalNoOfHitsBattleAreaCanTake += (int)ship.TypeOfShip;
                    if(!(ship.TypeOfShip == ShipType.P || ship.TypeOfShip == ShipType.Q))
                    {
                        throw new Exception("Type of ship should be either {‘P’, ‘Q’} ");
                    }
                    if(totalNoOfHitsBattleAreaCanTake>(HeightOfBattleArea*WidthOfBattleArea))
                    {
                        throw new Exception("No Of BatteShips should be less than (Width * Height) of BattleArea");
                    }
                }
            }
        }

        [Obsolete("DO NOT USE THIS METHOD",false)]
        // Updates the entire battle field area according to the list of ships and their relative position.
        public void UpdateEntireBattleFieldArea(SortedList<string, Ship> list)
        {
            foreach (KeyValuePair<string, Ship> element in list)
            {
                UpdateBattleFieldCells(element.Key, element.Value);
            }
        }

        // Checks and Returns if the Missle has successfully Hit in True or False and sets the nextPlayer who shall fire
        public bool MissileHitOrMiss(string address, string currentPlayer, out int nextPlayer)
        {
            bool result = false;
            if (totalNoOfHitsBattleAreaCanTake > 0)
            {
                int row = Helper.AlphabetToNo(address) - 1;
                int column = Convert.ToInt32(address.Substring(1, 1)) - 1;

                if (Area[row, column].NoOfHitsItCanTake > 0)
                {
                    Area[row, column].NoOfHitsItCanTake--;
                    Console.WriteLine("{0} fires a missile with target {1} which got hit", currentPlayer, address.Substring(0, 2));
                    result = true;
                    totalNoOfHitsBattleAreaCanTake--;
                    nextPlayer = Convert.ToInt32(currentPlayer.Substring(7, 1));
                }
                else
                {
                    Console.WriteLine("{0} fires a missile with target {1} which got miss", currentPlayer, address.Substring(0, 2));
                    result = false;
                    nextPlayer = Convert.ToInt32(name.Substring(7, 1));
                }
            }
            else
            {
                Console.WriteLine("{0} won the battle", currentPlayer);
                nextPlayer = 0;
            }
            return result;
        }
    }
}
