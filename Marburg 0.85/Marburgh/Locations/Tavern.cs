using System;
using System.Threading;

public class Tavern
{
    public static int wager;
    public static void Inn(Creature p)
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "You enter a bustling tavern. More flavor will be added soon describing the place and what you can do.");
        Console.WriteLine("\n\n[G]amble                    [L]ocal gossip");
        if (Event.TFRescued == true && Marburgh.Program.tutorial == false) Console.WriteLine("[T]alk to the bartender     " + Colour.TIME +"[S]"+ Colour.RESET+"peak with rescued Townfolk\n[R]eturn\n");
        else Console.WriteLine("[T]alk to the bartender     [R]eturn\n");
        Utilities.EmbedColourText(Colour.GOLD, "You have ", $"{p.gold} ", "gold\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "g")
            Gamble(p);
        else if (choice == "l")
            Gossip();
        else if (choice == "t")
            Bartender();
        else if (choice == "r")
            Marburgh.Program.GameTown();
        else if(choice == "s" && Event.TFRescued && Marburgh.Program.tutorial == false)
        {
            Console.Clear();
            if (Dungeon.DungeonInfo[0].boss.IsAlive)
            {
                Console.WriteLine("One of the townsfolk approaches, while the others regard you, gratitude in their eyes.");
                Utilities.ColourText(Colour.SPEAK, $"\n'Thank you so much {p.family.FirstName} for rescuing us!\nWe were sure we were going to die in there.\nIf only that horrible Orc was dead we would be able to start rebuilding our village'");
                Utilities.Keypress();
            }
            else
            {
                Console.WriteLine("The townspeople thank you, one by one, relief on all of their faces.\nThe last person to thank you, the mayor of the town pulls you aside a moment");
                Utilities.ColourText(Colour.SPEAK, $"\n'Thank the gods you were able to save us all.\nWe will start rebuilding immediately.\nIn time, our town will be as strong and prosperous as it ever was.\nI am worried tho. I believe that this was only the beginning\nThere are monsters and terrors out there eager to stake a claim on our land'");
                Utilities.Keypress();
                Console.Clear();
                Console.WriteLine("The town is rebuilding\nPeople are going back to work!\nThe weapon shop has opened!\nThe armor shop has opened!\nShops are getting more supplies!\nConstruction on the bank has begun!");
                if (Time.day >3) Time.Events.Add(new DayEvent("Bank Construction", false, true, false, "Construction is complete!\nThe " + Colour.GOLD + "bank " + Colour.RESET + "has been built!", Time.day -3, Time.week +1, Time.month, Time.year));
                else Time.Events.Add(new DayEvent("Bank Construction", false, true, false, "Construction is complete!\nThe " + Colour.GOLD + "bank " + Colour.RESET + "has been built!",Time.day + 2, Time.week,Time.month,Time.year));
                Marburgh.Program.tutorial = true;
                Utilities.Keypress();
            }
        }
        Inn(p);
    }

    private static void Bartender()
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "The Bartender will have more to say once dungeoning has been implemented");
        Utilities.Keypress();
    }

    private static void Gossip()
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "Word is this game's gonna be pretty cool when it gets finished");
        Utilities.Keypress();
    }

    private static void Gamble(Creature p)
    {   
        if (p.gold > 0)
        {
            Console.Clear();
            Console.WriteLine("You head towards the back of the tavern.\nHaving grown up in Marburgh, you know where all the games are, legal and otherwise");
            Console.WriteLine("What do you feel like playing? \n\n[B]lackjack       [D]ice      [T]hree card monty     \n[R]eturn");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "r") Inn(p);
            else if (choice != "b" && choice != "t" && choice != "d") Gamble(p);
            Wager(p);
            if (choice == "b") BlackJackGame.StartBlackJack(p, wager);
            if (choice == "d") DiceGame.Dice(p, wager);
            if (choice == "t") ThreeCardMonteGame.ThreeCardMonte(p, wager);
        }
        else Console.WriteLine("You don't have enough money!");
        Utilities.Keypress();             
    }

    private static void Wager(Creature p)
    {
        Console.Clear();
        do
        {
            Utilities.EmbedColourText(Colour.GOLD, "You have ", $"{p.gold}", " gold\nHow much would you like to wager?\n\n[0] Return\n" + Colour.GOLD);
        } while (!int.TryParse(Console.ReadLine(), out wager));
        Console.WriteLine(Colour.RESET);
        if (wager == 0) Inn(p);
        else if (wager > 150 * p.level)
        {
            Console.WriteLine("You can't gamble that much");
            Utilities.Keypress();
            Wager(p);
        }
        else if (p.gold >= wager)
        {
            Utilities.EmbedColourText(Colour.GOLD, "\nYou want to wager ", $"{wager}", " gold?\n\n[Y]es     [N]o");
            string wagerConfirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (wagerConfirm != "y") Wager(p);
            else p.gold -= wager;            
        }
    }   
}