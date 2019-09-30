using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Combat
{
    public static List<bool> playerText = new List<bool> { };
    public static List<string> combatText = new List<string> { };
    public static int round;    
    public static string[] currentAttackOptions = new string[] {"","","","" };
    public static int Attacks = 2;
    public static int GoldReward;
    public static int XPReward;
    public static int target = 0;
    public static List<Drop> DropList = new List<Drop> { };
    //Temprary mitigation from defend
    public static int tempMit;
    //Is this a boss fight?
    public static bool bossFight;

    public static void GameCombat(Creature p, List<Monster> monster, Dungeon d)
    {
        round = 0;
        //reset rewards
        GoldReward = XPReward = 0;
        bossFight = false;
        DropList = new List<Drop> { };
        //If you desecrated runes, cut health in half then set back to false
        foreach (Monster mon in monster)
        {
            mon.bleed = 0;
            mon.burning = 0;
            for (int i = 0; i < 2; i++)
            {
                mon.stun[i] = 0;
            }
            ////mon.health += mon.addHP;
            ////mon.damage += mon.addDam;
            mon.health = SpecialEvent.desecrated ? mon.health /= 2 : mon.health;
        }
        SpecialEvent.desecrated = false;
        //Check if it's a boss fight
        for (int i = 0; i < Boss.BossList.Count(); i++)
        {
            if (monster[0].name == Boss.BossList[i].name) bossFight = true;
        }        
        CombatStart(p, monster, d);
    }

    public static void CombatStart(Creature p, List<Monster> monster, Dungeon d)
    {
        round++;
        target = 0;
        StartUpdate(p,monster);
        if (p.canAct) PlayerActionSelect(p, monster, d);
        else UIStunned(p,monster);
        MonsterActionSelect(p, monster, d);
        Utilities.Keypress();
        CombatStart(p, monster, d);
    }

    public static void DamagePlayer(string text, string text2,Creature p, Dungeon d, Monster mon, int damage)
    {
        damage = (damage < 0) ? 0 : damage;
        if (text != "") Console.WriteLine(text);
        Console.WriteLine(text2);
        p.health -= damage - p.Armor.effect - tempMit;
        p.health = (p.health <= 0) ? 0 : p.health;
        PlayerDeathCheck(p, mon);        
        tempMit = 0;
    }

    private static void PlayerDeathCheck(Creature p, Monster mon)
    {
        if (p.health == 0)
        {
            Utilities.ColourText(Colour.DAMAGE, $"You were killed by the {mon.name}\n");
            Utilities.Keypress();
            Utilities.Death();
        }
    }

    public static void DamageMonster(string text, string text2, Creature p, Dungeon d, List<Monster> monster, int damage)
    {        
        UIActionSelect(p, monster);
        Console.SetCursorPosition(0, 9);
        PlayerStatusDamage(p, d);        
        monster[target].health -= damage;
        if (text != "") Console.WriteLine(text);
        Console.WriteLine(text2);
        Thread.Sleep(300);
        KillCheck(p, d, monster[target], monster);
    }

    private static void KillCheck(Creature p, Dungeon d,Monster mon, List<Monster> monster)
    {
        if (mon.health <= 0)
        {
            Utilities.ColourText(Colour.DAMAGE, $"You killed the {mon.name}!\n");
            GoldReward += mon.gold;
            XPReward += mon.xp;
            int dropRoll = Utilities.rand.Next(1, 101);
            if (dropRoll <= mon.drop.dropChance) DropList.Add(mon.drop);
            monster.Remove(mon);
            Thread.Sleep(300);
        }
        DeathCheck(d, p, monster);
    }

    public static void MonsterStatusDamage(Creature p, Dungeon d, Monster mon, List<Monster> monster)
    {
        if (mon.bleed > 0)
        {
            mon.health -= mon.bleedDam;
            Utilities.EmbedColourText(Colour.MONSTER, Colour.BLOOD, Colour.DAMAGE, "The ",$"{mon.name} ","", "bleeds ", "for ", $"{mon.bleedDam} ", "damage!");
        }
        if (mon.burning > 0)
        {
            mon.health -= mon.burnDam;
            Utilities.EmbedColourText(Colour.MONSTER, Colour.BURNING, Colour.DAMAGE, "The ", $"{mon.name} ", "", "burns ", "for ", $"{mon.burnDam} ", "damage!");
        }
        KillCheck(p, d, mon, monster);
    }

    public static void PlayerStatusDamage(Creature p, Dungeon d)
    {
        if (p.bleed >0)
        {
            p.health -= p.bleedDam;
            Utilities.EmbedColourText(Colour.BLOOD, Colour.DAMAGE, "You ", "bleed ", "for ", $"{p.bleedDam} ", "damage!");            
        }
        PlayerDeathCheck(p, null);
    }

    public static void SelectTarget(Creature p,List<Monster> monster, Dungeon d)
    {        
        if (monster.Count == 1) target = 0;
        else
        {
            UIActionSelect(p, monster);
            Console.SetCursorPosition(0, 18);
            for (int i = 0; i < monster.Count; i++)
            {
                Utilities.CenterText($"        [{i + 1}] " + Colour.MONSTER + $"{monster[i].name}  "+Colour.RESET);
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
            else if (choice == 0) PlayerActionSelect(p, monster, d);
            else SelectTarget(p, monster, d);
        }
    }

    private static void MonsterActionSelect(Creature p, List<Monster> monster, Dungeon d)
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
            if (hitRoll <= 70)
            {
                int attackRoll = Utilities.rand.Next(1, 101);
                if (attackRoll <= 70)
                {
                    DamagePlayer("", $"The " + Colour.MONSTER + $"{mon.name} " + Colour.RESET+ "hits you for " + Colour.DAMAGE + $"{mon.damage - p.Armor.effect - tempMit} " + Colour.RESET +"damage!", p, d, mon, mon.damage);
                }
                else if (attackRoll >= 71 && attackRoll <= 95)
                {
                    SpecialAttack.MonsterAttack(p, d, mon, monster, 1);
                }
                else
                {
                    SpecialAttack.MonsterAttack(p, d, mon, monster, 2);
                }
            }
            else Console.WriteLine($"The {mon.name} misses you!");            
        }
    }    

    private static void PlayerActionSelect(Creature p, List<Monster> monster, Dungeon d)
    {
        UIActionSelect(p, monster);
        Console.SetCursorPosition(50, 18);
        Console.Write("SELECT AN ACTION");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "h")
        {
            UIActionSelect(p, monster);
            if (p.health == p.maxHealth || p.potions < 1)
            {
                UIActionSelect(p, monster);
                Console.SetCursorPosition(50, 18);
                if (p.health == p.maxHealth) Utilities.ColourText(Colour.SPEAK, "You don't need healing!\n");
                else if (p.potions < 1) Utilities.ColourText(Colour.SPEAK, "You don't have enough potions!\n");
                Utilities.CenterText("Press any key to continue");
                Console.ReadKey(true);
                PlayerActionSelect(p, monster, d);
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
            PlayerActionSelect(p, monster, d);
        }
        else if (choice == "1")
        {
            int damage = p.damage + p.Weapon.damageEffect + p.Weapon.effect;
            SelectTarget(p, monster,d);
            string text = "You hit the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
            DamageMonster("", text, p, d, monster, damage);
        }
        else if (choice == "2")
        {
            int damage = (p.damage + p.Weapon.damageEffect + p.Weapon.effect) / 2;
            SelectTarget(p, monster, d);
            string text = "You get in a defensive stance, raising your mitigation and lowering your damage\nYou hit the " + Colour.MONSTER + $"{monster[target].name} " + Colour.RESET + "for " + Colour.DAMAGE + $"{damage} " + Colour.RESET + "damage!";
            tempMit = 2 * p.level;
            DamageMonster("", text, p, d, monster, damage);
        }
        else if (choice == "3" && currentAttackOptions[0] != "" && p.energy >0) SpecialAttack.Attack(p, d, monster, 3);        
        else if (choice == "4" && currentAttackOptions[1] != "") SpecialAttack.Attack(p, d, monster, 4);
        else if (choice == "5" && currentAttackOptions[2] != "") SpecialAttack.Attack(p, d, monster, 5);
        else if (choice == "6" && currentAttackOptions[3] != "") SpecialAttack.Attack(p, d, monster, 6);
        else if ((choice == "3" && currentAttackOptions[0] != "" || choice == "4" && currentAttackOptions[1] != "" || choice == "5" && currentAttackOptions[2] != "" || choice == "6" && currentAttackOptions[3] != "") && p.energy > 0)
        {
            UIActionSelect(p, monster);
            Console.SetCursorPosition(50, 18);
            Utilities.EmbedColourText(Colour.ENERGY, "You don't have enough ", "energy", "!");
            PlayerActionSelect(p, monster, d);
        }
        else PlayerActionSelect(p, monster, d);
    }

    public static void StartUpdate(Creature p, List<Monster> monster)
    {
        p.canAct = true;
        for (int i = 0; i < p.stun.Length; i++)
        {
            if (p.stun[i] > 0) p.canAct = false;
            p.stun[i]--;
        }
        if (p.bleed > 0) p.bleed--;
        if (p.casting > 0) p.casting--;
        if (p.burning > 0) p.burning--;
        if (p.shield > 0) p.shield--;
        foreach (Monster mon in monster)
        {
            mon.declareAction = Attack.creatureUpdateName[2];
            for (int i = 0; i < mon.stun.Length; i++)
            {
                if (mon.stun[i] > 0) mon.stun[i]--;
                if (mon.stun[i] > 0) mon.declareAction = Attack.creatureUpdateName[i];
            }
            if (mon.bleed > 0) mon.bleed--;
            if (mon.casting > 0) mon.casting--;
            if (mon.burning > 0) mon.burning--;
            if (mon.shield > 0) mon.shield--;
            if (mon.confused > 0) mon.confused--;
        }
    }

    public static void UIActionSelect(Creature p, List<Monster> monster)
    {
        DrawOpponent(p, monster);
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
        Console.WriteLine($"\t\t{Colour.HEALTH}{ p.health}{Colour.RESET}/{Colour.HEALTH}{ p.maxHealth}\t\t\t\t\t{Colour.ENERGY}{p.energy}{Colour.RESET}/{Colour.ENERGY}{p.maxEnergy}\t\t\t\t{Colour.SP}{ p.magic}{Colour.RESET}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        combatText = new List<string> { };
        Utilities.CombatText("[1]Attack", "[2]Defend", currentAttackOptions[0]);
        Utilities.CombatText(currentAttackOptions[1], currentAttackOptions[2], currentAttackOptions[3]);
        Utilities.CombatText("[H]eal", "[C]haracter", "\n");
        string a = "", b = "", c = "", d = "";
        if (p.shield > 0) a = "SHIELDED\t";
        if (p.bleed > 0) b = "BLEEDING\t";
        if (p.burning > 0) c = "BURNING\t";
        if (p.defending == true) d = "DEFENDING";
        Console.Write(Colour.SHIELD + a + Colour.BLOOD + b + Colour.BURNING + c + Colour.MITIGATION + d + Colour.RESET);
    }

    public static void UIStunned(Creature p, List<Monster> monster)
    {
        DrawOpponent(p, monster);
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
        Console.WriteLine($"\t\t{Colour.HEALTH}{ p.health}{Colour.RESET}/{Colour.HEALTH}{ p.maxHealth}\t\t\t\t\t{Colour.ENERGY}{p.energy}{Colour.RESET}/{Colour.ENERGY}{p.maxEnergy}\t\t\t\t{Colour.SP}{ p.magic}{Colour.RESET}");
        Console.WriteLine("-----------------------------------------------1q-------------------------------------------------------------------------");
        Console.WriteLine("");
        Utilities.CenterText("YOU ARE STUNNED");
        Utilities.CenterText("Press any key to continue");
        Console.ReadKey(true);
        Console.SetCursorPosition(0, 9);
    }

    public static void DrawOpponent(Creature p, List<Monster> Monster)
    {
        string ActionColourChoice = Colour.ACTION;
        foreach (Monster mon in Monster)
        {
            for (int i = 0; i < mon.statusText.Length; i++)
            {
                mon.statusText[i] = "";
            }
            if (mon.special > 0) ActionColourChoice = Colour.SPECIAL;
            if (mon.bleed > 0) mon.statusText[0] = "BLEEDING";
            if (mon.burning > 0) mon.statusText[1] = "BURNING";
            if (mon.shield > 0) mon.statusText[2] = "SHIELDED";
            if (mon.confused > 0) mon.statusText[3] = "CONFUSED";
        }
        Console.Clear();
        Console.WriteLine($"Combat round {round}");
        if (Monster.Count == 1)
        {
            Console.Write(Colour.MONSTER);
            Utilities.CenterText(Monster[0].name);
            Console.Write(Colour.HEALTH);
            Utilities.CenterText(Monster[0].health.ToString());
            Console.Write(ActionColourChoice);
            Utilities.CenterText(Monster[0].declareAction);
            Console.Write(Colour.BLOOD);
            Utilities.CenterText(Monster[0].statusText[0]);
            Console.Write(Colour.BURNING);
            Utilities.CenterText(Monster[0].statusText[1]);
            Console.Write(Colour.SHIELD);
            Utilities.CenterText(Monster[0].statusText[2]);
            Console.Write(Colour.STUNNED);
            Utilities.CenterText(Monster[0].statusText[3]);
            Console.Write(Colour.RESET);
        }
        else if (Monster.Count == 2)
        {
            Console.Write(Colour.MONSTER);
            Utilities.CenterText(Monster[0].name, Monster[1].name);
            Console.Write(Colour.HEALTH);
            Utilities.CenterText(Monster[0].health.ToString(), Monster[1].health.ToString());
            Console.Write(ActionColourChoice);
            Utilities.CenterText(Monster[0].declareAction, Monster[1].declareAction);
            Console.Write(Colour.BLOOD);
            Utilities.CenterText(Monster[0].statusText[0], Monster[1].statusText[0]);
            Console.Write(Colour.BURNING);
            Utilities.CenterText(Monster[0].statusText[1], Monster[1].statusText[1]);
            Console.Write(Colour.SHIELD);
            Utilities.CenterText(Monster[0].statusText[2], Monster[1].statusText[2]);
            Console.Write(Colour.STUNNED);
            Utilities.CenterText(Monster[0].statusText[3], Monster[1].statusText[3]);
            Console.Write(Colour.RESET);
        }
        else if (Monster.Count == 3)
        {
            Console.Write(Colour.MONSTER);
            Utilities.CenterText(Monster[0].name, Monster[1].name, Monster[2].name);
            Console.Write(Colour.HEALTH);
            Utilities.CenterText(Monster[0].health.ToString(), Monster[1].health.ToString(), Monster[2].health.ToString());
            Console.Write(ActionColourChoice);
            Utilities.CenterText(Monster[0].declareAction, Monster[1].declareAction, Monster[2].declareAction);
            Console.Write(Colour.STATUS);
            Console.Write(Colour.BLOOD);
            Utilities.CenterText(Monster[0].statusText[0], Monster[1].statusText[0], Monster[2].statusText[0]);
            Console.Write(Colour.BURNING);
            Utilities.CenterText(Monster[0].statusText[1], Monster[1].statusText[1], Monster[2].statusText[1]);
            Console.Write(Colour.SHIELD);
            Utilities.CenterText(Monster[0].statusText[2], Monster[1].statusText[2], Monster[2].statusText[2]);
            Console.Write(Colour.STUNNED);
            Utilities.CenterText(Monster[0].statusText[3], Monster[1].statusText[3], Monster[2].statusText[3]);
            Console.Write(Colour.RESET);
        }
    }
    public static void DeathCheck(Dungeon d, Creature p, List<Monster> monster)
    {
        if (monster.Count == 0)
        {
            ResetBuffsDebuffs(p);
            Reward.FightReward(d, p);
        }
    }        
    public static void ResetBuffsDebuffs(Creature p)
    {
        p.bleed = 0;
        p.casting = 0;
        p.burning = 0;
        p.shield = 0;
        p.canAct = true;
    }
}