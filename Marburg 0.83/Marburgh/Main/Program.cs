using System;
using System.Threading;
using System.Collections.Generic;

namespace Marburgh
{
    class Program
    {
        public static bool bank;
        public static bool tutorial;
        public static Dungeon d = new Dungeon("", 1, "", 0, 0, Room.StarterRoomList, Room.StarterRoomList, Room.StarterRoomList, Boss.BossList[0], false,  1, false, Shell.StarterDungeonShell);
        //STARTS GAME
        static void Main(string[] args)
        {
            Welcome();
        }

        //GAME FUNCTIONS    
        public static void Welcome()
        {
            Colour.SetupConsole();
            Console.Clear();
            Utilities.ColourText(Colour.TIME,"/'\\_/`\\             ( )                       ( )    \n");
            Utilities.ColourText(Colour.XP,"|     |   _ _  _ __ | |_    _   _  _ __   __  | |__  \n");
            Utilities.ColourText(Colour.BOSS,"| (_) | /'_` )( '__)| '_`\\ ( ) ( )( '__)/'_ `\\|  _ `\\ \n");
            Utilities.ColourText(Colour.DROP,"| | | |( (_| || |   | |_) )| (_) || |  ( (_) || | | |\n");
            Utilities.ColourText(Colour.RAREDROP,"(_) (_)`\\__,_)(_)   (_,__/'`\\___/'(_)  `\\__  |(_) (_)\n");
            Utilities.ColourText(Colour.NAME,"                                       ( )_) |       \n");
            Utilities.ColourText(Colour.GOLD,"                                        \\___/'       \n");
            Utilities.ColourText(Colour.CLASS,"");
            Utilities.EmbedColourText(Colour.HEALTH,"\n\n","[N]","ew Game       ");//[L]oad game);
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "n")
            {
                Dungeon.CreateDungeon(Dungeon.DungeonInfo[0]);
                d = Dungeon.AvailableDungeon[0];
                Create.Character1Create();
            }
            //else if (choice == "l") Data.Load(p);
            else Welcome();
        }

        public static void Story()
        {
            Console.Clear();
            Console.WriteLine("You come from a long line of adventurers\n");
            Thread.Sleep(600);
            Console.WriteLine("Your mother, an adventurer herself was murdered by Orcs, as they ransacked your town\n");
            Thread.Sleep(600);
            Console.WriteLine("Many villagers died, and even more were captured. Until they are free, your town is but a shadow of its old self\n");
            Thread.Sleep(600);
            Console.WriteLine("Worse yet, the Savage Orc that leads them is getting stronger and building an army\n");
            Thread.Sleep(600);
            Console.WriteLine("In five days, your town will be overrun\n");
            Thread.Sleep(600);
            Console.WriteLine("Are you a bad enough dude to save them?");
            Utilities.Keypress();
        }

        public static void GameTown()
        {
            for (int i = 0; i < Time.Events.Count; i++) {if (Time.Events[i].name == "Bank Construction" && Time.Events[i].trigger && Time.Events[i].active) bank = true;}
            Console.Clear();
            Utilities.ColourText(Colour.SPEAK, "You are in the town of Marburgh. It is a small town, but is clearly growing. Who knows what will be here in a month?\n\n");
            if (tutorial) Console.WriteLine(        "[W]eapon shop            [A]rmor shop            [I]tem shop          [D]ungeon");
            else Console.WriteLine(                 "[I]tem shop              [D]ungeon");
            if (tutorial && bank) Console.WriteLine("[T]avern                 [Y]our house            [B]ank               [?]Help          ");
            else Console.WriteLine(                 "[T]avern                 [Y]our house            [?]Help    ");
            Console.WriteLine(                      "[C]haracter              [L]evel Master          [O]ther Places       [Q]uit                 ");      
            Utilities.ColourText(Colour.SPEAK, "\n\nWhat would you like to do?\n\n");
            Utilities.EmbedColourText(Colour.TIME, Colour.TIME, Colour.TIME, Colour.TIME, "It is day ",$"{Time.day}",", the ",$"{Time.weeks[Time.week]}", " week of ",$"{Time.months[Time.month]}",", ",$"{Time.year}","\n\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "w" && tutorial) Shop.GameShop(Shop.WeaponShop, Create.p);
            else if (choice == "a" && tutorial) Shop.GameShop(Shop.ArmorShop, Create.p);
            else if (choice == "d") Explore.DungeonCheck(d, Create.p);
            else if (choice == "i") Shop.ItemShop(Create.p);
            else if (choice == "?") Utilities.Help();
            else if (choice == "c") Utilities.CharacterSheet(Create.p);
            else if (choice == "l") LevelMaster.VisitLevelMaster(Create.p);
            else if (choice == "t") Tavern.Inn(Create.p);
            else if (choice == "y") House.YourHouse(Create.p);
            else if (choice == "b" && tutorial && bank) Bank.GameBank(Create.p);
            else if (choice == "q") Utilities.Quit();
            else if (choice == "x")
            {
                Create.p.xp += 50;
                Create.p.gold += 900;
                BlackJack.PlayBlackJack();
            }
            else if (choice == "o") OtherPlaces.Other(Create.p);
            GameTown();
        }        
    }
}