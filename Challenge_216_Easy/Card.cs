using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_216_Easy
{
    public class Card
    {
        public CardValue Value { get; set; }
        public Suit Suit { get; set; }
    }

    public enum Suit
    {
        Clubs = 1,
        Diamonds,
        Hearts,
        Spades
    }

    public enum CardValue
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}
