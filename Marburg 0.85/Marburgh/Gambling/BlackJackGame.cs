using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Card
{
    public int value;
    public string svalue;
    public string suit;
    public Card(string suit, string svalue, int value)
    {
        this.suit = suit;
        this.svalue = svalue;
        this.value = value;
    }
}

public class BlackJackGame
{
    public static Random rand = new Random();
    public static List<Card> playerHand = new List<Card> { new Card("Spades", "Ace", 11) };
    public static List<Card> dealerHand = new List<Card> { new Card("Spades", "Ace", 11) };
    public static List<Card> deck = new List<Card> { new Card("Spades", "Ace", 11) };
    public static string[] suits = new string[] { "Spades","Clubs","Diamonds","Hearts" };
    public static string[] svalues = new string[] { "n Ace", " Two", " Three", " Four", " Five", " Six", " Seven", "n Eight", " Nine", " Ten", " Jack", " Queen", " King" };
    public static int[] values = new int[] { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

    public static void StartBlackJack(Creature p, int wager)
    {
        SetUp();
        while (playerHand.Count < 2)
        {
            Deal(playerHand);
        }
        while (dealerHand.Count < 2)
        {
            Deal(dealerHand);
        }
        PlayBlackJack(p, wager);
    }

    public static void PlayBlackJack(Creature p, int wager)
    {
        Console.Clear();   
        for (int i = 0; i < playerHand.Count; i++)
        {
            Console.WriteLine($"You have a{playerHand[i].svalue} of {playerHand[i].suit}");
        }
        if (playerHand.Count < 2 && Count(playerHand) == 21)
        {
            Console.WriteLine("\nBlackjack! That pays 3 to 1!\n");
            Win(p, wager, 3);
            Utilities.Keypress();
            Tavern.Inn(p);
        }
        Console.WriteLine($"\nThat makes {Count(playerHand)}");
        if (Count(playerHand) > 21)
        {
            Console.WriteLine("You Bust!");
            Lose();
            Utilities.Keypress();
            Tavern.Inn(p);
        }
        Console.WriteLine($"\nThe dealer is showing a{dealerHand[0].svalue} of {dealerHand[0].suit}\n");
        Console.WriteLine("Would you like to [H]it or [S]tay?");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "h")
        {
            Deal(playerHand);
            PlayBlackJack(p, wager);
        }
        else if (choice == "s") Resolve(p, playerHand,dealerHand, wager);
        else PlayBlackJack(p,wager);
    }

    private static void SetUp()
    {
        deck.Clear();
        for (int i = 0; i < suits.Length; i++)
        {
            for (int x = 0; x < svalues.Length; x++)
            {
                deck.Add(new Card(suits[i], svalues[x], values[x]));
            }
        }
        playerHand.Clear();
        dealerHand.Clear();   
    }

    private static void Win(Creature p, int wager,int multiplier)
    {
        Console.WriteLine($"You win! You receive {wager * multiplier} gold!\n");
        p.gold += wager * multiplier;
    }

    private static void Resolve(Creature p, List<Card> playerHand, List<Card> dealerHand, int wager)
    {
        Console.WriteLine($"\n\nThe dealer flips his cards, revealing a{dealerHand[0].svalue} of {dealerHand[0].suit} and a{dealerHand[1].svalue} of {dealerHand[1].suit}");
        while (Count(dealerHand) < 17)
        {
            Deal(dealerHand);
            Console.WriteLine($"The dealer draws a{ dealerHand[dealerHand.Count-1].svalue} of { dealerHand[dealerHand.Count - 1].suit}");
        }
        if (Count(dealerHand) > 21)
        {
            Console.WriteLine("The dealer busts!");
            Win(p, wager, 2);
        }
        else
        {
            Console.WriteLine($"\nYour total is {Count(playerHand)}\nThe Dealer's total is {Count(dealerHand)}\n");
            if (Count(dealerHand) > Count(playerHand)) Lose();
            else if (Count(dealerHand) < Count(playerHand)) Win(p, wager, 2);
            else
            {
                Console.WriteLine("You break even! You get your money back!");
                p.gold += wager;
            }
        }
        Utilities.Keypress();
    }

    private static void Lose()
    {
        Console.WriteLine("\n\nYou Lose!\nWith a grin, the dealer takes your money\n");
    }

    private static void Deal(List<Card> hand)
    {
        int cardDraw = rand.Next(0, deck.Count-1);
        hand.Add(deck[cardDraw]);
        deck.RemoveAt(cardDraw);
    }

    public static int Count(List<Card> hand)
    {
        int aces = 0;
        int val = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            val += hand[i].value;
        }        
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].value == 11) aces++;            
        }
        while (val > 21 && aces > 0)
        {
            aces--;
            val -= 10;
        }
        return val;
    }
}