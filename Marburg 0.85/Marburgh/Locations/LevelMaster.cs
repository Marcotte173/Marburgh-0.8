using System;

class LevelMaster
{
    public static int[] xpRequired = new int[] {0, 30, 75, 125, 185, 255, 315, 385, 475, 575, 700, 830, 1000};

    public static void VisitLevelMaster(Creature p)
    {
        Console.Clear();
        {
            Utilities.ColourText(Colour.SPEAK, "The Level Master is meditating. He looks up at you.\n\n'Are you here to level up?\n");
            Console.WriteLine("\n[Y]es    [N]o\n");
            string level = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (level == "y")
            {
                if (p.xp < xpRequired[p.level]) Utilities.ColourText(Colour.SPEAK, "\nHe looks at you thoughtfully.\n'Hmmm... You're not QUITE ready yet'\nCome back when you are more experienced");
                else LevelUp(p);
            }
            else
                Utilities.ColourText(Colour.SPEAK, "\nQuit wasting my time!");
        }
        Utilities.Keypress();
    }

    public static void LevelUp(Creature p)
    {
        Console.Clear();
        p.xp -= xpRequired[p.level];
        p.level += 1;        
        Utilities.EmbedColourText(Colour.XP, "Congrats! You are level ", $"{p.level}", "!");
        Utilities.EmbedColourText(Colour.HEALTH, "\nMax health increased by ", $"{p.lvlHealth}", "!");
        Utilities.EmbedColourText(Colour.ENERGY, "Max energy increased by ", $"{p.lvlEnergy}", "!");
        Utilities.EmbedColourText(Colour.HIT, "\nHit increased by ", $"{p.lvlHit}", "!");
        Utilities.EmbedColourText(Colour.DAMAGE, "Damage increased by ", $"{p.lvlDamage}", "!");        
        Utilities.EmbedColourText(Colour.CRIT, "Crit increased by ", $"{p.lvlCrit}", "!");
        Utilities.EmbedColourText(Colour.MITIGATION, "\nMitigation increased by ", $"{p.lvlMitigation}", "!");        
        Utilities.EmbedColourText(Colour.DEFENCE, "Defence increased by ", $"{p.lvlDefence}", "!");
        if (p.pClass.startingMagic > 0)
        {
            Utilities.EmbedColourText(Colour.ABILITY, "Spellpower increased by ", $"{p.lvlMagic}", "!");
            p.maxMagic += p.pClass.startingMagic;
        }
        if (p.level == 2 && p.pClass != Create.Mage)
        {
            string b = p.attacks[0].Split(']')[1];
            Console.WriteLine($"\n\nYou learn a new"+ Colour.SHIELD + " Ability" + Colour.RESET + "!!!\nYou learn" + Colour.ABILITY + $" { b}" + Colour.RESET);
            Player.currentAttackOptions[0] = p.attacks[0];
        }
        p.maxEnergy += p.lvlEnergy;
        p.maxHealth += p.lvlHealth;
        p.health = p.maxHealth;
        p.energy = p.maxEnergy;
        p.damage += p.lvlDamage;
        p.mitigation += p.lvlMitigation;
        p.hit += p.lvlHit;
        p.crit += p.lvlCrit;
        p.defence += p.lvlDefence;
    }
}