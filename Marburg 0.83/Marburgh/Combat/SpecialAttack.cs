using System;
using System.Collections.Generic;

public class SpecialAttack
{
    public static void Attack(Creature p, Dungeon d, List<Monster> monster, int button)
    {
        if (p.pClass.cName == "Warrior")
        {
            int a = button;
            switch (a)
            {
                case 3:
                    p.energy--;
                    Combat.SelectTarget(p, monster,d);
                    int damage = (p.damage + p.Weapon.damageEffect + p.Weapon.effect) / 2;
                    string text = $"You rend the {monster[Combat.target].name} causing it to bleed!";
                    string text2 = $"You hit the " + Colour.MONSTER + $"{monster[Combat.target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
                    monster[Combat.target].bleed = 2;
                    monster[Combat.target].bleedDam = 3;
                    Combat.DamageMonster(text, text2, p, d, monster, damage);
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
                    Combat.SelectTarget(p, monster,d);
                    int damage = (p.damage + p.Weapon.damageEffect + p.Weapon.effect) / 3;
                    string text = $"You stun the {monster[Combat.target]}!";
                    string text2 = "You hit the " + Colour.MONSTER + $"{monster[Combat.target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
                    monster[Combat.target].stun[1] = 2;
                    Combat.DamageMonster(text, text2, p, d, monster, damage);
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
                    Combat.SelectTarget(p, monster,d);
                    string text = "You "+ Colour.BURNING + "blast " + Colour.RESET + "the " +Colour.MONSTER + $"{monster[Combat.target]} " + Colour.RESET + "with fire, " + Colour.BURNING + "burning " + Colour.RESET +"them!";
                    string text2 = "Your fireblast hits the " + Colour.MONSTER + $"{monster[Combat.target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
                    monster[Combat.target].burning = 2;
                    monster[Combat.target].burnDam = 3;
                    Combat.DamageMonster(text, text2, p, d, monster, damage);
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

    public static void MonsterAttack(Creature p, Dungeon d, Monster mon, List<Monster> monster,int button)
    {
        {
            int a = button;
            int b = mon.pClass.type;
            switch (b)
            {
                case 1: //Slime                    
                    switch (a)
                    {
                        case 1:
                            if (monster.Count < 3 && mon.health < (mon.maxHealth/2))
                            {
                                Console.WriteLine("The slime splits. There is a new slime!");
                                monster.Add(Monster.starterBestiary[0].MonsterCopy());
                                monster[monster.Count - 1].maxHealth = monster[monster.Count - 1].health = mon.health;
                            }
                            else Combat.DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.RESET + "hits you for " + Colour.DAMAGE + $"{mon.damage - p.Armor.effect - Combat.tempMit} " + Colour.RESET + "damage!", p, d, mon, mon.damage);
                            break;
                        case 2:
                            int damage = mon.damage * 2 - p.Armor.effect - Combat.tempMit;
                            Combat.DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.DAMAGE + "crits " + Colour.RESET + "you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);
                            break;
                    }
                    break;
                case 2: //Goblin
                    switch (a)
                    {
                        case 1:
                            Utilities.EmbedColourText(Colour.MONSTER,Colour.BLOOD,"The ","Goblin ","","rakes", " you with its claws");
                            Utilities.ColourText(Colour.BLOOD, "You are bleeding\n");
                            p.bleed = 2;
                            p.bleedDam = 2;
                            break;
                        case 2:
                            int damage = mon.damage * 2 - p.Armor.effect - Combat.tempMit;
                            Combat.DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.DAMAGE + "crits " + Colour.RESET + "you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);
                            break;
                    }
                    break;
                case 3://Kobold
                    switch (a)
                    {
                        case 1:
                            Console.WriteLine("The monster hits you with a special attack");
                            break;
                        case 2:
                            int damage = mon.damage * 2 - p.Armor.effect - Combat.tempMit;
                            Combat.DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.DAMAGE + "crits " + Colour.RESET + "you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);
                            break;
                    }
                    break;
                case 4: //Skeleton
                    switch (a)
                    {
                        case 1:
                            Console.WriteLine("The monster hits you with a special attack");
                            break;
                        case 2:
                            int damage = mon.damage * 2 - p.Armor.effect - Combat.tempMit;
                            Combat.DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.DAMAGE + "crits " + Colour.RESET + "you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);
                            break;
                    }
                    break;
                case 5: //Orc
                    switch (a)
                    {
                        case 1:
                            Console.WriteLine("The monster hits you with a special attack");
                            break;
                        case 2:
                            int damage = mon.damage * 2 - p.Armor.effect - Combat.tempMit;
                            Combat.DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.DAMAGE + "crits " + Colour.RESET + "you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);
                            break;
                    }
                    break;
            }            
        }
    }
}