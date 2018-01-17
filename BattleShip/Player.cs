using System;
using System.Collections.Generic;
namespace BattleShip
{
    public class Player
    {
        static int PlayerNo = 1;
        string name;
        int noOfEntries = 1;

        public Player(int noOfShips)
        {
            TotalNoOfShips = noOfShips;
            this.name = "Player-" + PlayerNo;
            PlayerNo++;
        }

        public string Name { get { return name; } }
        public int TotalNoOfShips { get; set; }


        SortedList<int, Ship> listOfShipAndPosition = new SortedList<int, Ship>();
        public SortedList<int, Ship> ListOfShipAndPosition { get { return listOfShipAndPosition; } }

        public void AddShips(string shipListAndPosition)
        {
            string[] stringArr = shipListAndPosition.Split(' ');
            listOfShipAndPosition.Add(noOfEntries, new Ship(stringArr[0], stringArr[1], stringArr[2]));
            noOfEntries++;
        }

        Queue<string> firingSequence = new Queue<string>();
        public Queue<string> FiringSequence { get { return firingSequence; } }


        public void AddMissileLaunchSequence(string playerfiringSequence)
        {
            string[] stringArr = playerfiringSequence.Split(' ');
            foreach (string str in stringArr)
            {
                firingSequence.Enqueue(str.Substring(0, 2));
            }
        }

    }

}
