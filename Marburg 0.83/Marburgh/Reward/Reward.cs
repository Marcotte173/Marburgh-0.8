using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
public class Reward
{
    public static void FightReward(Dungeon d, Creature p)
    {
        Utilities.Keypress();
        Console.Clear();        
        int goldroll = Utilities.rand.Next(-2, 6);
        int xproll = Utilities.rand.Next(-1, 3);
        double goldAdd = (Combat.GoldReward + (goldroll * d.tier)) * d.diminishingReturns;
        int gold = Convert.ToInt32(goldAdd);
        double xpAdd = Combat.XPReward + (xproll * d.tier) * d.diminishingReturns;
        int xp = Convert.ToInt32(xpAdd);
        Console.WriteLine("You have defeated your enemies\n");
        Console.WriteLine($"You find {gold} gold");
        p.gold += gold;
        Console.WriteLine($"You gain {xp} experience");
        p.xp += xp;
        if (p.xp >= LevelMaster.xpRequired[p.level]) Utilities.ColourText(Colour.XP, "YOU ARE ELIGIBLE FOR A LEVEL RAISE\n");
        //Get the drops on the drop list
        for (int i = 0; i < Combat.DropList.Count; i++)
        {
            //If you already have it, increase the amount. Otherwise, add it to the list
            Console.WriteLine($"You find a {Combat.DropList[i].name}");
            bool exists = false;
            for (int x = 0; x < p.Drops.Count; x++)
            {
                if (p.Drops[x] == Combat.DropList[i])
                {
                    p.Drops[x].amount++;
                    exists = true;
                    break;
                }
            }
            if (exists == false) p.Drops.Add(Combat.DropList[i]);
        }
        Utilities.Keypress();
        Explore.currentShell.encountered = true;
        if (Combat.bossFight) BossReward(d, p);
        Explore.GameDungeon(d, p);
    }

    private static void BossReward(Dungeon d, Creature p)
    {
        if (d.boss == Boss.BossList[0]) Time.Events[0].trigger = false;
        Console.Clear();
        d.boss.IsAlive = false;
        Console.WriteLine(d.boss.followUp);
        Utilities.Keypress();
        Explore.currentShell.current = false;
        Marburgh.Program.d.shell[1].current = true;
        Marburgh.Program.GameTown();
    }

    public static void TreasureLootTable(int tier, Creature p)
    {
        if (tier == 1)
        {
            Utilities.EmbedColourText(Colour.GOLD, Colour.XP, "The chest is filled with ","gold ", "and old ","books","!");
            Utilities.EmbedColourText(Colour.GOLD,"You find ","125 ","gold!");
            Utilities.EmbedColourText(Colour.XP,Colour.XP,"Reading the ","book ", "gains you ","15 ","experience!");
            p.gold += 125;
            p.xp += 15;
        }
    }

    public static void RoomSearch(Room room, Dungeon d, Creature p, List<Event> EventDisplay, int tier)
    {
        for (int i = 0; i < EventDisplay.Count; i++)
        {
            Thread.Sleep(500);
            if (EventDisplay[i].flavor == "Some" + Colour.GOLD + " gold" + Colour.RESET)
            {
                int goldAdd = Utilities.rand.Next(-3, room.modifier);
                p.gold += EventDisplay[i].effect + (goldAdd * 3);
                Utilities.EmbedColourText(Colour.GOLD, "You gain ", $"{EventDisplay[i].effect + (goldAdd * 6)}", " gold");
                Thread.Sleep(500);
            }
            if (EventDisplay[i].flavor == "A " + Colour.HEALTH + "potion" + Colour.RESET)
            {
                if (p.potions == p.maxPotions)
                    Console.WriteLine($"Somebody already drank the " + Colour.HEALTH + "potion" + Colour.RESET + "\nIt's just an empty bottle!\nOh well...");
                else
                {
                    p.potions++;
                    Console.Write($"You gain a " + Colour.HEALTH + "potion" + Colour.RESET);
                }
                Thread.Sleep(500);
            }
            if (EventDisplay[i].flavor == "An old " + Colour.XP + "book" + Colour.RESET)
            {
                int XPAdd = Utilities.rand.Next(-3, room.modifier);
                p.xp += EventDisplay[i].effect + XPAdd;
                Console.WriteLine($"Reading the " + Colour.XP + "book" + Colour.RESET + ", you gain insight into the dungeon and its inhabitants");
                Console.WriteLine($"You gain " + Colour.XP + $"{ EventDisplay[i].effect + XPAdd} " + Colour.RESET + "experience");
                Thread.Sleep(500);
            }
        }
    }
}