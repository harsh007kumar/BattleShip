using System;
namespace BattleShip
{
    public class Ship
    {
        // Constructor to Set default values of data memebers
        public Ship(string sh, string width = "1", string height = "1")
        {
            TypeOfShip = (ShipType)Enum.Parse(typeof(ShipType), sh);
            NoOfSuccessfulHits = 0;
            IsDestroyed = false;
            ShipWidth = Convert.ToInt32(width);
            ShipHeight = Convert.ToInt32(height);
        }

        // Property to take count of no of successful missiles hitting the ship
        public int NoOfSuccessfulHits { get; set; }

        // Returns if the ship is destroyed or not 
        public bool IsDestroyed { get; set; }

        // Return Width of Ship
        public int ShipWidth { get; set; }

        // Return Height of Ship
        public int ShipHeight { get; set; }

        // Type of Ship Ptype/QType
        public ShipType TypeOfShip { get; set; }

        // Return bool value in true false indicating ship is destroyed or not
        public virtual bool? IsShipDestroyed()
        {
            bool? isDestroyed;
            switch (TypeOfShip)
            {
                case ShipType.P:
                    {
                        if (NoOfSuccessfulHits >= 1)
                        { isDestroyed = true; }
                        else
                        { isDestroyed = false; }
                        break;
                    }
                case ShipType.Q:
                    {
                        if (NoOfSuccessfulHits >= 2)
                        { isDestroyed = true; }
                        else
                        { isDestroyed = false; }
                        break;
                    }
                default:
                    isDestroyed = null;
                    break;
            }
            return isDestroyed;
        }
    }
}



