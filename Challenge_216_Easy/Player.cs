using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_216_Easy
{
    public class Player
    {
        public List<Card> Cards = new List<Card>();
            public string Name = "";
            public Hand Hand = Hand.Nothing;
            public List<Card> HighestHand = new List<Card>();

            public void DetermineHand(List<Card> communityCards)
            {
                var currentHand = new List<Card>();
                currentHand.AddRange(this.Cards);
                currentHand.AddRange(communityCards);
                currentHand = currentHand.OrderByDescending(row => row.Value).ThenByDescending(row => row.Suit).ToList();

                var groupedBySuite = (from card in currentHand.OrderByDescending(row => row.Suit).ThenByDescending(row => row.Value)
                                      group card by card.Suit into g
                                      select g).ToList();

                //to determine Pairs/TwoPairs/Triples/FullHouse/FourOfAKind/straight
                var groupedByValue = (from card in currentHand
                                      group card by card.Value into g
                                      select g).ToList();

                var pairs = groupedByValue.Where(row => row.Count() == 2).ToList();
                var trips = groupedByValue.Where(row => row.Count() == 3).ToList();
                var fourOfAKind = groupedByValue.Where(row => row.Count() == 4).ToList();
                var straight = groupedByValue.ToList();

                //pairs or two pairs
                if (pairs.Count >= 1)
                {
                    this.HighestHand.Add(pairs[0].ElementAt(0));
                    this.HighestHand.Add(pairs[0].ElementAt(1));
                    if (pairs.Count == 1)
                    {
                        this.Hand = Hand.Pair;
                    }
                    else if (pairs.Count >= 2)
                    {
                        this.Hand = Hand.TwoPair;
                        this.HighestHand.Add(pairs[1].ElementAt(0));
                        this.HighestHand.Add(pairs[1].ElementAt(1));
                    }
                }

                if (trips.Count >= 1 && pairs.Count == 0)
                {
                    this.HighestHand.Clear();
                    for (int i = 0; i <= 2; i++)
                    {
                        this.HighestHand.Add(trips[0].ElementAt(i));
                    }
                    this.Hand = Hand.Triple;
                }

                //Straight
                if (straight.Count >= 5) //If there are at least 5 groups, this mean there are 5 unique card values
                {
                    int j = 0;
                    while (straight.Count >= 5 && j < 4)
                    {
                        var current = (int)straight[j].Select(row => row.Value).First();
                        var next = (int)straight[j + 1].Select(row => row.Value).First();
                        if (next == current - 1)
                        {
                            if (j == 3) //if 4 checks have come back true, hand contains straight
                            {
                                this.Hand = Hand.Straight;
                                foreach (var c in straight)
                                {
                                    this.HighestHand.Clear();
                                    this.HighestHand.Add(c.First());
                                }
                                break;
                            }
                            j += 1;
                        }
                        else
                        {
                            straight.RemoveAt(0);
                            j = 0; //reset counter
                        }
                    }
                }



                int counter = 0;
                while (this.HighestHand.Count < 5)
                {
                    if (!this.HighestHand.Contains(currentHand[counter]))
                    {
                        this.HighestHand.Add(currentHand[counter]);
                    }
                    counter++;
                }
            }
    }

    public enum Hand
    {
        Nothing = 0,
        Pair,
        TwoPair,
        Triple,
        Straight,
        Flush,
        FullHouse,
        StraightFlush,
        FourOfAKind,
        RoyalFlush
    }
}
