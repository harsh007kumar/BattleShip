using System;
using System.Collections.Generic;
using System.IO;

namespace BattleShip
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                #region TakeINPUT
                int lineNoCurrentBeingRead = 1, typeOfPC;
                // Dictonary which hold all the lines read from User Input File with Key as Line Number
                Dictionary<int, string> userInput = Helper.TakingInput(ref lineNoCurrentBeingRead, out typeOfPC);
                #endregion

                // Setting up BattleArea for both the Players
                BattleArea Player1BattleField = new BattleArea(Helper.CharacterToNo(userInput[lineNoCurrentBeingRead][0]), userInput[lineNoCurrentBeingRead][2]);
                BattleArea Player2BattleField = new BattleArea(Helper.CharacterToNo(userInput[lineNoCurrentBeingRead][0]), userInput[lineNoCurrentBeingRead][2]);

                // Creating profile for both players
                lineNoCurrentBeingRead++;
                int noOfShips = Helper.CharacterToNo(userInput[lineNoCurrentBeingRead][0]);
                Player P1 = new Player(noOfShips);
                Player P2 = new Player(noOfShips);


                // Preparing the List of ships and their position for each player
                for (int i = 0; i < noOfShips; i++)
                {
                    lineNoCurrentBeingRead++;
                    P1.AddShips(userInput[lineNoCurrentBeingRead]);
                    P2.AddShips(userInput[lineNoCurrentBeingRead]);
                    Player1BattleField.UpdateBattleFieldCells(userInput[lineNoCurrentBeingRead].Substring(6, 2), P1.ListOfShipAndPosition[i + 1]);
                    Player2BattleField.UpdateBattleFieldCells(userInput[lineNoCurrentBeingRead].Substring(9, 2), P2.ListOfShipAndPosition[i + 1]);
                }

                // Recording Missile Firing Sequence for each player
                lineNoCurrentBeingRead++;
                P1.AddMissileLaunchSequence(userInput[lineNoCurrentBeingRead]);
                lineNoCurrentBeingRead++;
                P2.AddMissileLaunchSequence(userInput[lineNoCurrentBeingRead]);

                #region GameStarts
                //=========== GAME STARTS HERE ===========//
                int rununtil = 1;
                while (rununtil != 0)
                {
                    switch (rununtil)
                    {
                        case 1:
                            if (P1.FiringSequence.Count > 0)
                            {
                                if (Player2BattleField.MissileHitOrMiss(P1.FiringSequence.Dequeue(), P1.Name, out rununtil))
                                { }
                            }
                            else
                            {
                                Console.WriteLine("{0} has no more missiles left to launch", P1.Name);
                                rununtil += 1;
                            }
                            break;

                        case 2:
                            if (P2.FiringSequence.Count > 0)
                            {
                                if (Player1BattleField.MissileHitOrMiss(P2.FiringSequence.Dequeue(), P2.Name, out rununtil))
                                { }
                            }
                            else
                            {
                                Console.WriteLine("{0} has no more missiles left to launch", P2.Name);
                                rununtil += 1;
                            }
                            break;
                        default:
                            {
                                if (P1.FiringSequence.Count + P2.FiringSequence.Count == 0)
                                {
                                    rununtil = 0;
                                    Console.WriteLine("\n\nNO MORE MISSILES LEFT TO LAUNCH, NOBODY WON!!");
                                }
                                else
                                { rununtil = 1; }
                            }
                            break;
                    }
                }
                #endregion

                if (typeOfPC == 1)
                {
                    Console.ReadKey();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("“Something went wrong!!\nPlease check the whether the Input file is not Empty & not currently Open\n" +
                                  "Also check if it's saved on appropriate path mentioned in Instructions\n\n" + ex.Message);
            }
            catch (Exception ex)
            { Console.WriteLine("“Something went terribly wrong. Please come back later.\n" + ex.Message); }
        }
    }
}







