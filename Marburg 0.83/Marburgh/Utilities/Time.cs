﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Time
{
    public static int day = 1;
    public static int week = 1;
    public static int month = 1;
    public static int year = 345;
    public static string[] weeks = new string[] { "none", "first", "second" };
    public static string[] months = new string[] { "none", "Janbruarch", "ApmaJune", "Jaugtempber", "Octvemdec" };

    public static List<DayEvent> Events = new List<DayEvent> 
    {
        new DayEvent("First Boss Attack", true, true, true, Colour.BOSS + "Time has run out.\nThe Savage orc's forces have overrun your town.", 1, 2, 1, 345)
    };

    public static void DayChange(int amount, Creature p)
    {
        int upOrDown = Utilities.rand.Next(0, 2);
        double rateChange = Utilities.rand.NextDouble();
        rateChange /= 100;
        day += amount;
        for (int i = 0; i < amount; i++)
        {
            Bank.bankRate = (upOrDown == 0) ? Bank.bankRate - rateChange : Bank.bankRate + rateChange;
        }
        p.investmentTerm -= amount;
        Utilities.Refresh(p);
        PassingOfTime();
    }

    public static void PassingOfTime()
    {
        int addweek = 0;
        int addmonth = 0;
        int addyear = 0;
        while (day > 5)
        {
            day -= 5;
            addweek++;
        }
        week += addweek;
        while (week > 2)
        {
            week -= 2;
            addmonth++;
        }
        month += addmonth;
        while (month > 4)
        {
            month -= 4;
            addyear++;
        }
        year += addyear;
        Bank.InvestCheck();
        DayCheck();
    }

    private static void DayCheck()
    {
        for (int i = 0; i < Events.Count; i++)
        {
            Events[i].active = false;
        }
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i].day == day && Events[i].week == week && Events[i].month == month && Events[i].year == year)
            {
                Events[i].active = true;
                if (Events[i].active && Events[i].trigger)
                {
                    Console.Clear();
                    Console.WriteLine($"{Events[i].flavor}");
                    Utilities.Keypress();
                    if (Events[i].gameOver) Utilities.GameOver();
                }                
            }
        }
    }
}