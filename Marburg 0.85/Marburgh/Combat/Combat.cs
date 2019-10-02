using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Combat
{
    public static List<bool> playerText = new List<bool> { };
    public static List<string> combatText = new List<string> { };
    public static int round;       
    public static int Attacks = 2;
    public static int GoldReward;
    public static int XPReward;    
    public static List<Drop> DropList = new List<Drop> { };
    //Temprary mitigation from defend
    public static int tempDef;
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
            mon.health = RandomEvent.desecrated ? mon.health /= 2 : mon.health;
        }
        RandomEvent.desecrated = false;
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
        Player.target = 0;
        StartUpdate(p,monster);
        Exchange(p,monster,d);        
        Utilities.Keypress();
        CombatStart(p, monster, d);
    }

    private static void Exchange(Creature p, List<Monster> monster, Dungeon d)
    {
        if (p.canAct)
        {
            Player.ActionSelect(p, monster, d);
            Console.WriteLine();
        }
        else UIStunned(p, monster);
        MonsterAI.ActionSelect(p, monster, d);
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
            mon.declareAction = Status.creatureUpdateName[2];
            for (int i = 0; i < mon.stun.Length; i++)
            {
                if (mon.stun[i] > 0) mon.stun[i]--;
                if (mon.stun[i] > 0) mon.declareAction = Status.creatureUpdateName[i];
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
        Console.WriteLine($"\t\t{Colour.HEALTH}{ p.health}{Colour.RESET}/{Colour.HEALTH}{ p.maxHealth}\t\t\t\t\t{Colour.ENERGY}{p.energy}{Colour.RESET}/{Colour.ENERGY}{p.maxEnergy}\t\t\t\t{Colour.ABILITY}{ p.magic}{Colour.RESET}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        combatText = new List<string> { };
        Utilities.CombatText("[1]Attack", "[2]Defend", Player.currentAttackOptions[0]);
        Utilities.CombatText(Player.currentAttackOptions[1], Player.currentAttackOptions[2], Player.currentAttackOptions[3]);
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
        Console.WriteLine($"\t\t{Colour.HEALTH}{ p.health}{Colour.RESET}/{Colour.HEALTH}{ p.maxHealth}\t\t\t\t\t{Colour.ENERGY}{p.energy}{Colour.RESET}/{Colour.ENERGY}{p.maxEnergy}\t\t\t\t{Colour.ABILITY}{ p.magic}{Colour.RESET}");
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
            
    public static void ResetBuffsDebuffs(Creature p)
    {
        p.bleed = 0;
        p.casting = 0;
        p.burning = 0;
        p.shield = 0;
        p.canAct = true;
    }
}