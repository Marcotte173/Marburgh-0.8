using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


public class SpecialEvent
{
    public static bool desecrated;
    public static void Event1(Dungeon d, Creature p, Event e)
    {
        Console.Clear();
        Console.WriteLine(e.flavor);
        Event.TFRescued = true;
        Explore.currentShell.encountered = true;
        Utilities.Keypress();
        Explore.GameDungeon(d, p);
    }

    public static void Event2(Dungeon d, Creature p, Event e)
    {
        Console.Clear();
        bool key = false;
        for (int i = 0; i < p.Drops.Count; i++)
        {
            if (p.Drops[i].name == "Chest Key" && p.Drops[i].amount > 0) key = true;
        }
        Console.WriteLine(e.flavor);
        Utilities.EmbedColourText(Colour.DAMAGE,"You could ", "bash"," the lock, but risk destroying the contents");
        Utilities.EmbedColourText(Colour.SP,"You could"," pick"," the lock, but in the time it takes, more monsters may find you");
        Utilities.EmbedColourText(Colour.NAME,"If only you had a"," key","!\n\n");
        Utilities.EmbedColourText(Colour.DAMAGE, Colour.SP, Colour.NAME, "","[B]","ash the lock        ","[P]","ick the lock             ","[K]","ey            [R]eturn");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "b")
        {
            Console.Clear();
            Utilities.ColourText(Colour.DAMAGE, "BLAM!");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            int bashRoll = Utilities.rand.Next(1, 101);
            if (bashRoll <= e.success + e.effect)
            {
                Console.WriteLine("Success!\n\n");
                Thread.Sleep(300);
                Reward.TreasureLootTable(d.tier,p);
                Utilities.Keypress();
            }
            else
            {
                Console.WriteLine("Failure!");
                Thread.Sleep(300);
                Console.WriteLine("It looks like the valuables inside were fragile indeed. Oh well, maybe next time");
                Utilities.Keypress();
            }
        }
        else if (choice == "p")
        {
            Console.Clear();
            Utilities.ColourText(Colour.SP, "CHICK CHICK!");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            int pickRoll = Utilities.rand.Next(1, 101);
            if (pickRoll <= e.success)
            {
                Console.WriteLine("Success!\n\n");
                Thread.Sleep(300);
                Reward.TreasureLootTable(d.tier,p);
                Utilities.Keypress();
            }
            else if (pickRoll > e.success && pickRoll <= e.success + e.effect)
            {
                Console.WriteLine("You got in!\n\n");
                Thread.Sleep(300);
                Reward.TreasureLootTable(d.tier,p);
                Utilities.Keypress();
                Console.WriteLine("\nThat took a while though, it looks like someone found you!");
                p.force = true;
                Utilities.Keypress();
                Console.Clear();
                Monster.Summon(Room.StarterSpecialRoomList[2], d, p);
            }
            else
            {
                Console.WriteLine("Failure!");
                Thread.Sleep(300);
                Console.WriteLine("Not only could you not get in, you took so long that someone found you!");
                p.force = true;
                Utilities.Keypress();
                Console.Clear();
                Monster.Summon(Room.StarterSpecialRoomList[2], d, p);
            }
        }
        else if (choice == "k" && key == true)
        {
            Console.Clear();
            Utilities.ColourText(Colour.NAME, "CLICK!");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            Console.WriteLine("Success!\n\n");
            Thread.Sleep(300);
            Console.WriteLine("Inside you find a bunch of treasure, to be described later!");
            Utilities.Keypress();
        }
        else if (choice == "k" && key == false)
        {
            Utilities.EmbedColourText(Colour.NAME, "\n\nYou don't have a ", "key", "!");
            Utilities.Keypress();
            Event1(d, p, e);
        }
        else if (choice == "r")
        {
            Explore.currentShell.encountered = true;
            Explore.GameDungeon(d, p);
        }
        else if (choice == "x")
        {
            p.Drops.Add(new Drop("Chest Key", 1, 100, 1));
            Event1(d, p, e);
        }
        else Event1(d, p, e);
        Explore.currentShell.encountered = true;
        Explore.GameDungeon(d, p);
    }    

    public static void Event3(Dungeon d, Creature p, Event e)
    {
        Console.Clear();
        Console.WriteLine(e.flavor);
        Utilities.EmbedColourText(Colour.ENERGY, "You could ", "study ", "the runes, trying to learn the secrets of the Orc gods");
        Utilities.EmbedColourText(Colour.DAMAGE, "You could ", "desecrate ", "the runes, angering the orcs but possibly interrupting their power source");
        Utilities.EmbedColourText(Colour.MITIGATION, "You could ", "walk away, ", "moving on to the next room\n\n");
        Utilities.EmbedColourText(Colour.ENERGY, Colour.DAMAGE, Colour.MITIGATION, "", "[S]", "tudy        ", "[D]", "esecrate             ", "[W]", "alk away\n\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "s")
        {
            Console.Clear();
            Utilities.ColourText(Colour.ENERGY, "Studying");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            int roll = Utilities.rand.Next(1, 101);
            if (roll <= 75)
            {
                if (p.health < p.maxHealth)
                {
                    Utilities.ColourText(Colour.ENERGY, "Success! ");
                    Utilities.EmbedColourText(Colour.HEALTH, "Your ", "health ", "returns to maximum!");
                    p.health = p.maxHealth;
                }
                else Console.WriteLine("Sadly, you glean very little from the runes");
            }
            else
            {
                Utilities.ColourText(Colour.DAMAGE, "This was not for you to know! It's too much for your mind or body!");
                p.health = 1;
                Utilities.EmbedColourText(Colour.HEALTH, Colour.DAMAGE, "Your ", "health ", "is reduced to ", "1", "!");
            }
            Utilities.Keypress();
            Explore.currentShell.encountered = true;
            Explore.GameDungeon(d, p);
        }
        if (choice == "d")
        {
            Console.Clear();
            Utilities.ColourText(Colour.DAMAGE, "Desecrating");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            int roll = Utilities.rand.Next(1, 101);
            if (roll <= 25)
            {
                Utilities.ColourText(Colour.HEALTH, "Success! ");
                desecrated = true;
                Utilities.EmbedColourText(Colour.HEALTH, "You hear screams from further in the dungeon!\nNext fight, every monster starts with", " HALF ", "health!");
            }
            else
            {
                Utilities.ColourText(Colour.DAMAGE, "You have made the gods angry!");
                Utilities.EmbedColourText(Colour.HEALTH, Colour.DAMAGE, "Your ", "health ", "is reduced to ", "1", "!");
            }
            Utilities.Keypress();
            Explore.currentShell.encountered = true;
            Explore.GameDungeon(d, p);
        }
        if (choice == "w")
        {
            Explore.currentShell.encountered = true;
            Explore.GameDungeon(d, p);
        }
        else Event3(d, p, e);
    }

    public static void Event4(Dungeon d, Creature p, Event e)
    {
        List<Monster> mon = new List<Monster> { new Monster("Elite Orc", pClass.MonsterClassList[4], 8, 65, 5, 0, Drop.BossDrop[0]) };
        Console.Clear();
        Console.WriteLine(e.flavor);
        Combat.CombatStart(p, mon, d);
        Explore.currentShell.encountered = true;
    }
}