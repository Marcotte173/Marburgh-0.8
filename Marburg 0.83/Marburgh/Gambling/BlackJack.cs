using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BlackJack
{
    public static Random rand = new Random();
    public static List<Card> playerHand = new List<Card> { new Card("Spades", "Ace", 11) };
    public static List<Card> dealerHand = new List<Card> { new Card("Spades", "Ace", 11) };
    public static List<Card> card = new List<Card> { new Card("Spades", "Ace", 11) };
    public static Deck deck = new Deck(card);
    public static string[] suits = new string[] { "Spades","Clubs","Diamonds","Hearts" };
    public static string[] svalues = new string[] { "n Ace", " Two", " Three", " Four", " Five", " Six", " Seven", "n Eight", " Nine", " Ten", " Jack", " Queen", " King" };
    public static int[] values = new int[] { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

    public static void CreateDeck()
    {
        card.Clear();
        for (int i = 0; i < suits.Length; i++)
        {
            for (int x = 0; x < svalues.Length; x++)
            {
                card.Add(new Card(suits[i], svalues[x], values[x]));
            }
        }
        deck = new Deck(card);
    }

    public static void PlayBlackJack()
    {        
        CreateDeck();
        playerHand.Clear();
        dealerHand.Clear();
        while (playerHand.Count < 2)
        {
            int cardDraw = rand.Next(0, card.Count);
            playerHand.Add(card[cardDraw]);
            card.RemoveAt(cardDraw);
        }
        while (dealerHand.Count < 2)
        {
            int cardDraw = rand.Next(0, card.Count);
            dealerHand.Add(card[cardDraw]);
            card.RemoveAt(cardDraw);
        }
        PlayerChoice();        
    }

    private static void PlayerChoice()
    {
        Console.Clear();
        int aces = 0;
        int totaleValue = 0;
        for (int i = 0; i < playerHand.Count; i++)
        {
            if (playerHand[i].value == 11) aces++;
        }
        for (int i = 0; i < playerHand.Count; i++)
        {
            Console.WriteLine($"You have a{playerHand[i].svalue} of {playerHand[i].suit}");
            totaleValue += playerHand[i].value;
        }
        while (totaleValue > 21 && aces > 0)
        {
            aces--;
            totaleValue -= 10;
        }
        Console.WriteLine($"That makes {totaleValue}");
        if (totaleValue > 21) Bust();
        Utilities.Keypress();
        int cardDraw = rand.Next(0, card.Count);
        playerHand.Add(card[cardDraw]);
        card.RemoveAt(cardDraw);
        PlayerChoice();
    }

    private static void Bust()
    {
        Console.WriteLine("\n\nYou Lose!");
        Utilities.Keypress();
    }
}