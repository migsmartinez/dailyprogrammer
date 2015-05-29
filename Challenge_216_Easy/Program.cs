using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_216_Easy
{
    class Program
    {
        static List<Card> Deck = new List<Card>();
        static List<Card> CommunityCards = new List<Card> ();
        static List<Player> Players = new List<Player>();
        static Random randomNum = new Random();

        static void Main(string[] args)
        {
            Console.Write("Number of players? (2-8): ");
            int numPlayers = int.Parse(Console.ReadLine());
            GenerateDeck();
            ShuffleDeck();
            InitialDeal(numPlayers);
            
            //Hands
            foreach(Player p in Players)
            {
                Console.Write(p.Name + "'s Hand: ");
                foreach(Card c in p.Cards)
                {
                    Console.Write(c.Value + " of " + c.Suit + " ");
                }
                Console.WriteLine();
            }

            //Flop
            Console.Write("Flop: ");
            GetCard();
            for(int i = 0; i < 3; i++)
            {
                Card c = GetCard();
                CommunityCards.Add(c);
                Console.Write(c.Value + " of " + c.Suit + " ");
            }
            //Turn
            Console.WriteLine();
            GetCard();
            Card turn = GetCard();
            CommunityCards.Add(turn);
            Console.WriteLine("Turn: " + turn.Value + " of " + turn.Suit);
            //River
            GetCard();
            Card river = GetCard();
            CommunityCards.Add(river);
            Console.WriteLine("River: " + river.Value + " of " + river.Suit);

            foreach(Player p in Players)
            {
                p.DetermineHand(CommunityCards);
                Console.WriteLine("Player " + p.Name + ": " + p.Hand);
            }

            Console.ReadKey();
        }

        static void GenerateDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardValue cv in Enum.GetValues(typeof(CardValue)))
                {
                    Card c = new Card();
                    c.Suit = suit;
                    c.Value = cv;
                    Deck.Add(c);

                }
            }
        }

        static void ShuffleDeck()
        {
            for(int i = 0; i < Deck.Count; i++)
            {
                int j = randomNum.Next(i, Deck.Count - 1);
                var temp = Deck[i];
                Deck[i] = Deck[j];
                Deck[j] = temp;
            }
        }

        static void InitialDeal(int numPlayers)
        {
            //2 loops, 1 card per loop to simulate actual deal
            for(int i = 1; i <= numPlayers; i++)
            {
                Player p = new Player();
                p.Name = "Player " + i;
                Players.Add(p);
                p.Cards.Add(GetCard());
            }
            foreach(Player p in Players)
            {
                p.Cards.Add(GetCard());
            }
        }

        static Card GetCard()
        {
            int indexToPull = randomNum.Next(0, Deck.Count - 1);
            Card c = Deck[indexToPull];
            Deck.RemoveAt(indexToPull);
            return c;
        }
    }
}
