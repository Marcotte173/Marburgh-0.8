using System;
using System.Threading;
public class ThreeCardMonteGame
{
    public static void ThreeCardMonte(Creature p, int wager)
    {
        Console.Clear();
        Console.WriteLine("You walk up to the table. You recognize the man behind the table, showing onlookers his cups and balls. \nHe's a cheat but he's so sure he's going to win he's offered 3 to 1 odds!");
        int ball = Utilities.rand.Next(1, 4);
        Console.WriteLine("After moving the balls around, you are pretty sure you've kept track");
        Utilities.ColourText(Colour.SPEAK,"\n'Well, what do you think? Which cup is it in?' \n[1] [2] or [3]?\n");
        int choice;
        do
        {
            
        } while (!int.TryParse(Console.ReadLine(), out choice));
        Console.WriteLine();
        Utilities.DotDotDot();
        if (choice == ball)
        {
            Console.WriteLine("\nThe ball is there!");
            Console.WriteLine($"You win! The man looks shocked. \nClearly unhappy, he gives you {wager *3} gold");
            p.gold += wager * 3;
        }
        else
        {
            Console.WriteLine("\nThere's nothing there!");
            Console.WriteLine($"You lose! Smiling smugly, the man takes your gold");
        }
    }
}