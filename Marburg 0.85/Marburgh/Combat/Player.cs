using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Player
{
    public static int target = 0;
    public static string[] currentAttackOptions = new string[] { "", "", "", "" };
    public static void PlayerDeathCheck(Creature p, Monster mon)
    {
        if (p.health == 0)
        {
            if (mon == null) Utilities.ColourText(Colour.DAMAGE, $"You died!\n");
            else Utilities.ColourText(Colour.DAMAGE, $"You were killed by the {mon.name}\n");
            Utilities.Keypress();
            Utilities.Death();
        }
    }

    public static void DamageMonster(string text, string text2, Creature p, Dungeon d, List<Monster> monster, int damage)
    {
        Combat.UIActionSelect(p, monster);
        Console.SetCursorPosition(0, 9);
        PlayerStatusDamage(p, d);
        monster[target].health -= damage;
        if (text != "") Console.WriteLine(text);
        Console.WriteLine(text2);
        Thread.Sleep(300);
        MonsterAI.KillCheck(p, d, monster[target], monster);
    }    

    public static void PlayerStatusDamage(Creature p, Dungeon d)
    {
        if (p.bleed > 0)
        {
            p.health -= p.bleedDam;
            Utilities.EmbedColourText(Colour.BLOOD, Colour.DAMAGE, "You ", "bleed ", "for ", $"{p.bleedDam} ", "damage!");
        }
        PlayerDeathCheck(p, null);
    }

    public static void SelectTarget(Creature p, List<Monster> monster, Dungeon d)
    {
        if (monster.Count == 1) target = 0;
        else
        {
            Combat.UIActionSelect(p, monster);
            Console.SetCursorPosition(0, 18);
            for (int i = 0; i < monster.Count; i++)
            {
                Utilities.CenterText($"        [{i + 1}] " + Colour.MONSTER + $"{monster[i].name}  " + Colour.RESET);
            }
            Console.Write("");
            Utilities.CenterText("Please choose a monster");
            int choice;
            do
            {

            } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out choice));
            if (choice == 1) target = 0;
            else if (choice == 2) target = 1;
            else if (choice == 3 && monster.Count == 3) target = 2;
            else if (choice == 0) ActionSelect(p, monster, d);
            else SelectTarget(p, monster, d);
        }
    }

    public static void ActionSelect(Creature p, List<Monster> monster, Dungeon d)
    {
        Combat.UIActionSelect(p, monster);
        Console.SetCursorPosition(50, 18);
        Console.Write("SELECT AN ACTION");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "h")
        {
            Combat.UIActionSelect(p, monster);
            if (p.health == p.maxHealth || p.potions < 1)
            {
                Combat.UIActionSelect(p, monster);
                Console.SetCursorPosition(50, 18);
                if (p.health == p.maxHealth) Utilities.ColourText(Colour.SPEAK, "You don't need healing!\n");
                else if (p.potions < 1) Utilities.ColourText(Colour.SPEAK, "You don't have enough potions!\n");
                Utilities.CenterText("Press any key to continue");
                Console.ReadKey(true);
                ActionSelect(p, monster, d);
            }
            else
            {
                Console.SetCursorPosition(0, 9);
                p.health = p.maxHealth;
                p.potions -= 1;
                Utilities.ColourText(Colour.HEALTH, "You heal to full health\n");
            }
        }
        else if (choice == "c")
        {
            Utilities.CharacterSheet(p);
            ActionSelect(p, monster, d);
        }
        else if (choice == "1")
        {
            SelectTarget(p, monster, d);
            int hitRoll = Utilities.rand.Next(1, 101);
            if (hitRoll <= (p.hit - monster[target].defence))
            {
                int critRoll = Utilities.rand.Next(1, 101);
                int damage = (critRoll <= p.crit) ? (p.damage + p.Weapon.damageEffect + p.Weapon.effect) * 2 : p.damage + p.Weapon.damageEffect + p.Weapon.effect;
                string a = (critRoll <= p.crit) ? Colour.CRIT + "crit" : Colour.RESET + "hit";
                DamageMonster("", "You " + $"{a} " + "the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, monster, damage);
            }
            else
            {
                Combat.UIActionSelect(p, monster);
                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"You miss the {monster[target].name}!");
            }
        }
        else if (choice == "2")
        {
            int damage = (p.damage + p.Weapon.damageEffect + p.Weapon.effect) / 2;
            SelectTarget(p, monster, d);
            string text = "You get in a defensive stance, raising your defence and lowering your damage\nYou hit the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
            Combat.tempDef = (p.pClass.type == 3)? 5 * p.level: 3 * p.level;
            DamageMonster("", text, p, d, monster, damage);
        }
        else if (choice == "3" && currentAttackOptions[0] != "" && p.energy > 0) Player.Attack(p, d, monster, 3);
        else if (choice == "4" && currentAttackOptions[1] != "") Player.Attack(p, d, monster, 4);
        else if (choice == "5" && currentAttackOptions[2] != "") Player.Attack(p, d, monster, 5);
        else if (choice == "6" && currentAttackOptions[3] != "") Player.Attack(p, d, monster, 6);
        else if ((choice == "3" && currentAttackOptions[0] != "" || choice == "4" && currentAttackOptions[1] != "" || choice == "5" && currentAttackOptions[2] != "" || choice == "6" && currentAttackOptions[3] != "") && p.energy > 0)
        {
            Combat.UIActionSelect(p, monster);
            Console.SetCursorPosition(50, 18);
            Utilities.EmbedColourText(Colour.ENERGY, "You don't have enough ", "energy", "!");
            ActionSelect(p, monster, d);
        }
        else ActionSelect(p, monster, d);
    }

    public static void Attack(Creature p, Dungeon d, List<Monster> monster, int button)
    {
        if (p.pClass.cName == "Warrior")
        {
            int a = button;
            switch (a)
            {
                case 3:
                    p.energy--;
                    SelectTarget(p, monster,d);
                    int damage = (p.damage + p.Weapon.damageEffect + p.Weapon.effect) / 2;
                    string text = $"You rend the {monster[target].name} causing it to bleed!";
                    string text2 = $"You hit the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
                    monster[target].bleed = 2;
                    monster[target].bleedDam = 3;
                    DamageMonster(text, text2, p, d, monster, damage);
                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
            }
        }

        if (p.pClass.cName == "Rogue")
        {
            int a = button;
            switch (a)
            {
                case 3:
                    p.energy--;
                    SelectTarget(p, monster,d);
                    int damage = (p.damage + p.Weapon.damageEffect + p.Weapon.effect) / 3;
                    string text = $"You stun the {monster[target]}!";
                    string text2 = "You hit the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
                    monster[target].stun[1] = 2;
                    DamageMonster(text, text2, p, d, monster, damage);
                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
            }
        }

        if (p.pClass.cName == "Mage")
        {
            int a = button;
            switch (a)
            {
                case 3:
                    p.energy--;
                    int damage = p.damage + p.magic;
                    SelectTarget(p, monster,d);
                    string text = "You "+ Colour.BURNING + "blast " + Colour.RESET + "the " +Colour.MONSTER + $"{monster[target]} " + Colour.RESET + "with fire, " + Colour.BURNING + "burning " + Colour.RESET +"them!";
                    string text2 = "Your fireblast hits the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
                    monster[target].burning = 2;
                    monster[target].burnDam = 3;
                    DamageMonster(text, text2, p, d, monster, damage);
                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
            }
        }             
    }       
}