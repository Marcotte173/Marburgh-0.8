using System;
using System.Threading;
public class DiceGame
{
    public static void Dice(Creature p, int wager)
    {        
        int playerRoll = Utilities.rand.Next(1, 7);
        int opponentRoll = Utilities.rand.Next(1, 7);
        Console.Clear();
        Utilities.EmbedColourText(Colour.GOLD, "Confident, you set ", $"{wager}", " gold on the table");
        Console.Write($"\nYou roll a die, it comes up.");
        RollDice(playerRoll);
        Console.Write($"\nYour opponent rolls a die, it comes up.");
        RollDice(opponentRoll);
        string report = (playerRoll == opponentRoll) ? "\n\nIt's a tie!\nYou take your money back" : (playerRoll > opponentRoll) ? "\n\nYou win!\nYou receive " + Colour.GOLD + wager * 2 + Colour.RESET + " gold!" : "\n\nYou lose!\n";
        Console.WriteLine(report);
        p.gold = (playerRoll == opponentRoll) ? p.gold + wager : (playerRoll > opponentRoll) ? p.gold + 2 * wager : p.gold;
        Utilities.Keypress();
        return;        
    }
    public static void RollDice(int roll)
    {
        Thread.Sleep(300);
        Console.Write($".");
        Thread.Sleep(300);
        Console.Write($".");
        Thread.Sleep(300);
        Console.Write($"{roll}!");
        Thread.Sleep(400);
    }
}