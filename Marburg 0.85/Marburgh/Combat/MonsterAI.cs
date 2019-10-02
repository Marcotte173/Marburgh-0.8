using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class MonsterAI
{
    public static void ActionSelect(Creature p, List<Monster> monster, Dungeon d)
    {
        foreach (Monster mon in monster.ToList())
        {
            MonsterStatusDamage(p, d, mon, monster);
        }
        foreach (Monster mon in monster.ToList())
        {
            if (mon.stun[0] > 0)
            {
                Console.WriteLine($"The {mon.name} is frozen!");
                break;
            }
            if (mon.stun[1] > 0)
            {
                Console.WriteLine($"The {mon.name} is stunned!");
                break;
            }
            int hitRoll = Utilities.rand.Next(1, 101);
            if (hitRoll <= (mon.hit - p.defence - Combat.tempDef))
            {
                int attackRoll = Utilities.rand.Next(1, 101);
                if (attackRoll <= 75)
                {
                    int critRoll = Utilities.rand.Next(1, 101);
                    int damage = (critRoll <= mon.crit)?(mon.damage - p.Armor.effect - p.mitigation) *2 : mon.damage - p.Armor.effect - p.mitigation;
                    string a = (critRoll <= mon.crit)? Colour.CRIT + "crits" : Colour.RESET + "hits";
                    damage = (damage < 0) ? 0 : damage;
                    DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} "+ $"{a} " + Colour.RESET + "you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);
                }                
                else
                {
                    MonsterAI.MonsterAttack(p, d, mon, monster, 1);
                }
            }
            else Console.WriteLine($"The {mon.name} misses you!");
        }
    }

    public static void MonsterStatusDamage(Creature p, Dungeon d, Monster mon, List<Monster> monster)
    {
        if (mon.bleed > 0)
        {
            mon.health -= mon.bleedDam;
            Utilities.EmbedColourText(Colour.MONSTER, Colour.BLOOD, Colour.DAMAGE, "The ", $"{mon.name} ", "", "bleeds ", "for ", $"{mon.bleedDam} ", "damage!");
        }
        if (mon.burning > 0)
        {
            mon.health -= mon.burnDam;
            Utilities.EmbedColourText(Colour.MONSTER, Colour.BURNING, Colour.DAMAGE, "The ", $"{mon.name} ", "", "burns ", "for ", $"{mon.burnDam} ", "damage!");
        }
        KillCheck(p, d, mon, monster);
    }

    public static void KillCheck(Creature p, Dungeon d, Monster mon, List<Monster> monster)
    {
        if (mon.health <= 0)
        {
            Utilities.ColourText(Colour.DAMAGE, $"You killed the {mon.name}!\n");
            Combat.GoldReward += mon.gold;
            Combat.XPReward += mon.xp;
            int dropRoll = Utilities.rand.Next(1, 101);
            if (dropRoll <= mon.drop.dropChance) Combat.DropList.Add(mon.drop);
            monster.Remove(mon);
            Thread.Sleep(300);
        }
        DeathCheck(d, p, monster);
    }

    public static void DeathCheck(Dungeon d, Creature p, List<Monster> monster)
    {
        if (monster.Count == 0)
        {
            Combat.ResetBuffsDebuffs(p);
            Reward.FightReward(d, p);
        }
    }

    public static void MonsterAttack(Creature p, Dungeon d, Monster mon, List<Monster> monster, int button)
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
                            int damage = mon.damage - p.Armor.effect - p.mitigation;
                            damage = (damage < 0) ? 0 : damage;
                            if (monster.Count < 3 && mon.health < (mon.maxHealth / 2))
                            {
                                Console.WriteLine("The slime splits. There is a new slime!");
                                monster.Add(Monster.starterBestiary[0].MonsterCopy());
                                monster[monster.Count - 1].maxHealth = monster[monster.Count - 1].health = mon.health;
                            }
                            else DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.RESET + "hits you for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!", p, d, mon, damage);                            
                            break;
                        case 2:
                            
                            break;
                    }
                    break;
                case 2: //Goblin
                    switch (a)
                    {
                        case 1:
                            Utilities.EmbedColourText(Colour.MONSTER, Colour.BLOOD, "The ", "Goblin ", "", "rakes", " you with its claws");
                            Utilities.ColourText(Colour.BLOOD, "You are bleeding\n");
                            p.bleed = 2;
                            p.bleedDam = 2;
                            break;
                        case 2:
                            
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
                            
                            break;
                    }
                    break;
                case 4: //Orc
                    switch (a)
                    {
                        case 1:
                            Console.WriteLine("The monster hits you with a special attack");
                            break;
                        case 2:
                            
                            break;
                    }
                    break;
                case 5: //Spider
                    switch (a)
                    {
                        case 1:
                            Console.WriteLine("The monster hits you with a special attack");
                            break;
                        case 2:
                           
                            break;
                    }
                    break;
                case 6: //Spider
                    switch (a)
                    {
                        case 1:
                            Console.WriteLine("The monster hits you with a special attack");
                            break;
                        case 2:
                           
                            break;
                    }
                    break;
            }
        }
    }

    public static void DamagePlayer(string text, string text2, Creature p, Dungeon d, Monster mon, int damage)
    {
        if (text != "") Console.WriteLine(text);
        Console.WriteLine(text2);
        p.health -= damage;
        p.health = (p.health <= 0) ? 0 : p.health;
        Player.PlayerDeathCheck(p, mon);
        Combat.tempDef = 0;
    }
}