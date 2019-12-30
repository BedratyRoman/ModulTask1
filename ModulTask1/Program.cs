using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulTask1
{
   
    struct Card

    {
        public string suit;
        public Weight weight;
        public Card (string suit, Weight weight)
        {

            this.suit = suit;
            this.weight = weight;

        }
            
    }




    enum Suit

    {
     Diamonds,
     Hearts,
     Spaders,
     Clubs

    }

    enum Weight
    {
     six,
     seven,
     eight,
     nine,
     ten,
     jack,
     lady,
     king,
     ase

    }

  

    class Program
    {

        static int ComputersVictory = 0;
        static int MyVictory = 0;
        static bool isComputer;
        static Card[] deck;

        public static void Main(string[] args)
        {
            
            deck = new Card[36];
            int k = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(Weight weight in Enum.GetValues(typeof(Weight)))
                {
                    Card carta = new Card(suit.ToString(), weight);
                    deck[k] = carta;
                    k++;
                }

            }
            foreach(Card card in deck)
            {
                Console.WriteLine(String.Format("Suit: {0}, Weight: {1}", card.suit, card.weight));
            }

            //Console.ReadKey();
            MashDeck();
            //Console.ReadKey();

            Console.WriteLine("-------------------------------------------------------------------------------");


            Random randomFirstStep = new Random();
            int chose = randomFirstStep.Next(0,100);
            isComputer = chose % 2 == 0 ? true : false;

            PlayGame(deck);
            startEnotherGame:
            Console.WriteLine("Do you want to play game one more time y/n?");
            string yn = Console.ReadLine();
            
            if (yn == "y")
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                isComputer = !isComputer;
                MashDeck();
                PlayGame(deck);
                goto startEnotherGame;
            }
            else
            {
                Console.WriteLine(String.Format("Computer has {0}  wins", ComputersVictory));
                Console.WriteLine(String.Format("I have {0}  wins", MyVictory));
            }



            Console.ReadKey();
        }

        public static void MashDeck()
        {
            Random random = new Random();
            for (int i = deck.Length - 1; i >= 0; --i)
            {
                int j = random.Next(i + 1);
                Card temp = deck[j];
                deck[j] = deck[i];
                deck[i] = temp;
            }

            //foreach (Card card in deck)
            //{
            //    Console.WriteLine(String.Format("Suit: {0}, Weight: {1}", card.suit, card.weight));
            //}
        }

        public static void PlayGame(Card[] deck)
        {
            int playerScore1 = 0;
            int playerScore2 = 0;

            playerScore1 = CountFirstTwoCard(deck[0].weight, deck[1].weight);
            if ((playerScore1 == 21) | (playerScore1 == 22))
            {
                if (isComputer)
                {
                    Console.WriteLine("Computer won this game ");
                    ComputersVictory++;
                    return;
                }
                else
                {
                    Console.WriteLine("i am won this game ");
                    MyVictory++;
                    return;
                }
            }


            playerScore2 = CountFirstTwoCard(deck[2].weight, deck[3].weight);
            if ((playerScore2 == 21) | (playerScore2 == 22))
            {
                if (!isComputer)
                {
                    Console.WriteLine("Computer won this game ");
                    ComputersVictory++;
                    return;
                }
                else
                {
                    Console.WriteLine("i am won this game ");
                    MyVictory++;
                    return;
                }
            }

            int index = 4;
            bool computerPass;
            bool playerPass;
            if (isComputer)
            {
                Console.WriteLine("Computer got two cards ");
            link:
                computerPass = false;
                playerPass = false;
                if (TakeNextCard(playerScore1))
                {

                    playerScore1 = playerScore1 + Counter(deck[index].weight);
                    if (playerScore1 == 21)
                    {
                        Console.WriteLine("Computer won this game got 21 score");
                        ComputersVictory++;
                        return;
                    }
                    index++;
                }
                else
                {
                    Console.WriteLine("Computer is pass");
                    computerPass = true;
                }
                Console.WriteLine("You have :" + playerScore2 + " score.");
                Console.WriteLine("Do you want to take a card?  y/n");
                string yn = Console.ReadLine();
                if (yn == "y")
                {
                    playerScore2 = playerScore2 + Counter(deck[index].weight);
                    if (playerScore2 == 21)
                    {
                        Console.WriteLine("i am won this game got 21 score");
                        MyVictory++;
                        return;
                    }
                    index++;

                }
                else
                {
                    Console.WriteLine("Player is pass");
                    playerPass = true;
                }

                if ((!computerPass) & (!playerPass) & playerScore1 < 21 & playerScore2 < 21)
                {
                    goto link;
                }
                else
                {
                    CountOfScore(playerScore1, playerScore2);
                }
            }

            else
            {
                Console.WriteLine("I got two cards: ");
            link1:
                computerPass = false;
                playerPass = false;
                Console.WriteLine("You have :" + playerScore1 + " score.");
                Console.WriteLine("Do you want to take a card?  y/n");
                string yn = Console.ReadLine();
                if (yn == "y")
                {
                    playerScore1 = playerScore1 + Counter(deck[index].weight);
                    if (playerScore1 == 21)
                    {
                        Console.WriteLine("i am won this game got 21 score");
                        MyVictory++;
                        return;
                    }
                    index++;

                }
                else
                {
                    Console.WriteLine("Player is pass");
                    playerPass = true;
                }

                if (TakeNextCard(playerScore2))
                {

                    playerScore2 = playerScore2 + Counter(deck[index].weight);
                    if (playerScore2 == 21)
                    {
                        Console.WriteLine("Computer won this game got 21 score");
                        ComputersVictory++;
                        return;
                    }
                    index++;
                }
                else
                {
                    Console.WriteLine("Computer is pass");
                    computerPass = true;
                }
                if ((!computerPass) & (!playerPass) & playerScore1 < 21 & playerScore2 < 21)
                {
                    goto link1;
                }
                else
                {
                    CountOfScore(playerScore2, playerScore1);
                }

            }


        }

        public static int Counter(Weight stringcard)
        {
            int score = 0;
            switch (stringcard)
            {
                case Weight.six:
                    score = 6;
                    break;
                case Weight.seven:
                    score = 7;
                    break;
                case Weight.eight:
                    score = 8;
                    break;
                case Weight.nine:
                    score = 9;
                    break;
                case Weight.ten:
                    score = 10;
                    break;
                case Weight.jack:
                    score = 2;
                    break;
                case Weight.lady:
                    score = 3;
                    break;
                case Weight.king:
                    score = 4;
                    break;
                case Weight.ase:
                    score = 11;
                    break;
            }
            return score;
        }

        public static int CountFirstTwoCard(Weight card1,Weight card2)
        {
            int sum = Counter(card1) + Counter(card2);
            return sum;
        }




        public static int CounScore(int sumCards, Weight nextCard)
        {
            return sumCards + Counter(nextCard);
        }


        public static bool TakeNextCard(int playerScore)
        {
            bool takeNext= false;
            if (playerScore < 15)
            {
                takeNext = true;
            }
            return takeNext;
        }
        public static void CountOfScore(int computerScore,int myScore)
        {

            Console.WriteLine("Computer got " + computerScore + " scores");
            Console.WriteLine("i got " + myScore + " scores");

            if (computerScore > 21 & myScore > 21)
            {
                if (computerScore > myScore)
                {
                    Console.WriteLine("i am won this game ");
                    MyVictory++;
                }

                else if (computerScore < myScore)
                {
                    Console.WriteLine("Computer won this game ");
                    ComputersVictory++;

                }

                else
                {

                    Console.WriteLine("Nobody won this game ");

                }
            }
            else if(computerScore>21&myScore<21)
            {
                Console.WriteLine("i am won this game ");
                MyVictory++;
            }

            else if (computerScore < 21 & myScore> 21)
            {
                Console.WriteLine("Computer won this game ");
                ComputersVictory++;
            }
            else
            {
                if (computerScore < myScore)
                {
                    Console.WriteLine("i am won this game ");
                    MyVictory++;
                }

                else if (computerScore > myScore)
                {
                    Console.WriteLine("Computer won this game ");
                    ComputersVictory++;

                }

                else
                {

                    Console.WriteLine("Nobody won this game ");

                }
            }

        }
    }
}
