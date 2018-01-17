using System;
namespace BattleShip
{
    public class Cell : Coordinates
    {
        public Cell()
        {
            IsOccupiedByShip = false;
            NoOfHitsItCanTake = 0;
        }

        public Cell(int hit)
        {
            IsOccupiedByShip = true;
            NoOfHitsItCanTake = hit;
        }
        public bool IsOccupiedByShip { get; set; }

        public int NoOfHitsItCanTake { get; set; }

    }
}
