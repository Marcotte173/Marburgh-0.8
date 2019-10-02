using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Help
{
    public static void General()
    {
        Console.Clear();
        Console.WriteLine("HELP\n\nWelcome to Marburgh\n\nThis is a fairly straightforward dungeon crawler.");
        Utilities.ColourText(Colour.NAME,"\nFamily\n");
        Console.WriteLine("\nYou are part of an adventuring family. Your family consists of 3 siblings.\nYou play as each sibling until their death or until the game ends.\nMoney in the bank when a sibling dies remains in the bank for the rest of the family");
        Console.WriteLine("If all of the siblings die, the game is over.\n");
        Utilities.ColourText(Colour.ITEM,"WEAPONS AND ARMOR\n\n");
        Console.WriteLine("Weapons and armor can be purchased at the Item shop now, and the Weapon and Armor shops when they open");
        Console.WriteLine("With an enhancement machine, you can use monster drops to enhance your weapons.\nYou can also use it to craft powerful weapons from boss drops");
        Utilities.ColourText(Colour.MONSTER, "\nDUNGEON\n\n");
        Console.WriteLine("Dungeons are navigated with the N,S,E, and W keys.\nIn each room you will face choices, random events and monsters");
        Console.WriteLine("When you find monsters, you will begin combat, where you use your unique skills to defeat them\nWatch out though, they have their own unique skills as well!");
        Utilities.Keypress();
        Console.Clear();
        int a = Create.p.pClass.type;
        switch (a)
        {
            case 1:
                Warrior();
                break;
            case 2:
                Rogue();
                break;
            case 3:
                Mage();
                break;
        }
        Utilities.Keypress();
        
    }

    private static void Mage()
    {
        Utilities.ColourText(Colour.CLASS, "Mage\n");
        Console.WriteLine("\nThe mage is a master of magic.\nWhile their damage and mitigation are low,Mages can hold 1 extra health potion and start with an ability");
        Utilities.ColourText(Colour.ABILITY, "\nFireblast");
        Utilities.EmbedColourText(Colour.BURNING,"\nThe mage blasts his target with a burst of fire, doing damage and causing ","burning","");
        Utilities.EmbedColourText(Colour.BURNING, "\n ","BURNING", "\n\n");
        Console.WriteLine("When a creature burns, they take x damage over y turns, affected by spellpower.");
    }

    private static void Rogue()
    {
        Utilities.ColourText(Colour.CLASS, "Rogue\n");
        Console.WriteLine("\nThe rogue is a master of combat.\nRogues focus on high damage and control abilities");
        Utilities.ColourText(Colour.ABILITY, "\nSTUN");
        Utilities.EmbedColourText(Colour.STUNNED, "\nThe rogue smacks his target with a sneaky strike, doing damage and causing ", "stun", "");
        Utilities.EmbedColourText(Colour.STUNNED, "\n ", "STUN", "\n\n");
        Console.WriteLine("When a creature is stunned, they cannot take an action.");
    }

    private static void Warrior()
    {
        Utilities.ColourText(Colour.CLASS, "Warrior\n");
        Console.WriteLine("\nThe mage is a master of defence.\nWarriors focus on high mitigation and damage abilities");
        Utilities.ColourText(Colour.ABILITY, "\nREND\n");
        Utilities.EmbedColourText(Colour.BLOOD, "\nThe warrior slice his target with a mighty blow, doing damage and causing ", "bleeding", "");
        Utilities.EmbedColourText(Colour.BLOOD, "\n", "BLEEDING\n", "");
        Console.WriteLine("When a creature bleeds, they take x damage over y turns.");
    }
}
