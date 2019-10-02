using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Explore
{
    //Variables, self explanatory
    public static Shell currentShell;
    public static bool canExplore = true;

    //Can you go exploring?
    public static void DungeonCheck(Dungeon d, Creature p)
    {
        //If no, go home
        if (canExplore == false)
        {
            Utilities.ColourText(Colour.SPEAK, "You are exhausted. Go to bed and come back tomorrow");
            Utilities.Keypress();
            Marburgh.Program.GameTown();
        }
        //Warning so you don't use it then leave right away
        Utilities.ColourText(Colour.SPEAK, "You may only go exploring once a day. Would you like to go now?");
        Console.WriteLine("\n\n[Y]es      [N]o");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "y")
        {
            //Disallow going back later, send to dungeon
            canExplore = false;
            GameDungeon(d, p);
        }
        Marburgh.Program.GameTown();
    }

   //Dungeon main screen
   public static void GameDungeon(Dungeon d, Creature p)
   {
        //Figure out what the current room is
        Console.Clear();              
        for (int i = 0; i < Shell.StarterDungeonShell.Length; i++)
        {
            if (Shell.StarterDungeonShell[i].current) currentShell = Shell.StarterDungeonShell[i];
        }
        //Display text based on flavor of the room. If it has been encountered already it gets the encountered text
        string a = (currentShell.encountered) ? currentShell.assignedRoom.flavor[1] : currentShell.assignedRoom.flavor[0];
        Console.WriteLine(a);
        //What happens in the new room
        if (currentShell.encountered == false) RoomSelection(d,p);
        //This is when you hit last room but boss is dead. You get treasure but less.
        else if (currentShell == d.shell[d.shell.Length - 1] && currentShell.encountered == true) 
        {
            int goldFound = Utilities.rand.Next(85, 120);
            //Pays off less according to diminishing returns
            double GoldGain = goldFound * d.diminishingReturns;
            int GoldGainInt = Convert.ToInt32(GoldGain);
            Console.Clear();
            Console.WriteLine($"You make it to the " + Colour.BOSS + d.boss.name + Colour.RESET + "'s lair.\nHe is long dead but you manage to find some treasure you didn't notice in your last pass");
            Console.WriteLine($"You gain " + Colour.GOLD + GoldGainInt + Colour.RESET + " gold and return home");
            p.gold += GoldGainInt;
            //Sets new diminihing returns, 25% less than last time
            if (d.diminishingReturns >0) d.diminishingReturns -= 0.25;
            //25% less chance to summon a monster
            if (d.monsterSummon>50) d.monsterSummon -= 25;
            Utilities.Keypress();
            //Make entrance current again
            currentShell.current = false;
            d.shell[1].current = true;
            //Reset the encounters for basic rooms
            Utilities.ResetNormalRooms();
            //Go back to town
            Marburgh.Program.GameTown(); 
        }
        //Navigation Options        
        Navigation(d, p);        
        GameDungeon(d, p);
    }

    private static void RoomSelection(Dungeon d , Creature p)
    {
        Utilities.Keypress();
        Console.Clear();
        //Check if you hit the boss room
        if (currentShell == d.shell[d.shell.Length - 1] )
        {
            //When you hit last room and boss is alive. FIGHT!
            Monster.opponentList = new List<Monster> { };
            Monster.opponentList.Add(d.boss.MonsterCopy());
            Combat.GameCombat(p, Monster.opponentList, d);
        }        
        //If not, move on
        bool random = false;
        bool specific = false;
        for (int i = 0; i < d.randomRoomOptions.Length; i++)
        {
            if (currentShell.assignedRoom.name == d.randomRoomOptions[i].name)
            {
                random = true;
                break;
            }
        }
        if (random == false)
        {
            for (int i = 0; i < d.staticRoomOptions.Length; i++)
            {
                if (currentShell.assignedRoom.name == d.staticRoomOptions[i].name)
                {
                    specific = true;
                    break;
                }
            }
        }
        if (specific)
        {
            int a = d.dungeonID;
            switch (a)
            {
                case 1:
                    CastleEvent.Room(d, p, currentShell.assignedRoom.EventArray);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    
                    break;
                case 6:
                    
                    break;
            }
            
        }
        else if (random) Room.RandomRoom(d, p, currentShell.assignedRoom.EventArray);
        else Room.RegularRoom(d, p, currentShell);
    }

    private static void Navigation(Dungeon d, Creature p)
    {
        if (p.xp >= LevelMaster.xpRequired[p.level]) Utilities.ColourText(Colour.XP, "YOU ARE ELIGIBLE FOR A LEVEL RAISE");
        //Navigation Display
        Console.SetCursorPosition(0, 20);
        //If North is an option
        if (currentShell.North > 0)
        {
            //Place a north option along with the name of the room or ??? if not discovered
            string a = (d.shell[currentShell.North].visited == true) ? d.shell[currentShell.North].assignedRoom.name : "???";
            string b = $"[N]orth - {a}";
            Utilities.CenterText($"[N]orth - {a}");
        }
        Console.WriteLine("\n\n");
        //If East or West are options
        if (currentShell.West > 0 || currentShell.East > 0)
        {
            //Place an East and/or West option along with the name of the room or ??? if not discovered
            string west = (currentShell.West == 0) ? "" : "[W]est - ";
            string westText = (currentShell.West == 0) ? "" : (d.shell[currentShell.West].visited == true) ? d.shell[currentShell.West].assignedRoom.name : "???";
            string east = (currentShell.East == 0) ? "" : "[E]ast - ";
            string eastText = (currentShell.East == 0) ? "" : (d.shell[currentShell.East].visited == true) ? d.shell[currentShell.East].assignedRoom.name : "???";
            Utilities.CenterText($"{west}{westText}", $"{east}{eastText}");
        }
        //If North is an option or it's the first room
        if (currentShell.South > 0 || currentShell == d.shell[1])
        {           
            Console.WriteLine("\n\n");
            //If it's the first room, give a return option
            if (currentShell == d.shell[1]) Utilities.CenterText("[R]eturn to town");
            else
            {
                //Otherwise place a South option along with the name of the room or ??? if not discovered
                string a = (d.shell[currentShell.South].visited == true) ? d.shell[currentShell.South].assignedRoom.name : "???";
                Utilities.CenterText($"[S]outh - {a}");
            }
        }
        Console.WriteLine();
        //Navigation options
        string choice = Console.ReadKey().KeyChar.ToString().ToLower();
        if (choice == "n" && currentShell.North > 0)
        {
            if (d.shell[currentShell.North].visited == false) ExploreNextRoom();
            ChangeShell(d, p, currentShell.North);
        }
        else if (choice == "s" && currentShell.South > 0)
        {
            if (d.shell[currentShell.South].visited == false) ExploreNextRoom();
            ChangeShell(d, p, currentShell.South);
        }
        else if (choice == "e" && currentShell.East > 0)
        {
            if (d.shell[currentShell.East].visited == false) ExploreNextRoom();
            ChangeShell(d, p, currentShell.East);
        }
        else if (choice == "w" && currentShell.West > 0)
        {
            if (d.shell[currentShell.West].visited == false) ExploreNextRoom();
            ChangeShell(d, p, currentShell.West);
        }
        else if (choice == "c") Utilities.CharacterSheet(p);
        else if (choice == "h") Utilities.Heal(p);
        else if (choice == "r" && currentShell == d.shell[1])
        {
            Utilities.ResetNormalRooms();
            Marburgh.Program.GameTown();
        }
        else if (choice == "r" && currentShell != d.shell[1])
        {
            Console.Clear();
            Console.WriteLine("You can only leave the dungeon from the entance");
            Utilities.Keypress();
        }
    }

    //Small flavor for when you haven't been to a room yet
    public static void ExploreNextRoom()
    {
        Console.Clear();
        Console.Write("Exploring");
        Utilities.DotDotDot();                
    }

    //Changes the current room
    public static void ChangeShell(Dungeon d, Creature p, int connect)
    {
        //Current shell isn't current anymore, the shell connection number is not current and visited 
        currentShell.current = false;
        d.shell[connect].current = true;
        d.shell[connect].visited = true;
        GameDungeon(d,p);
    }       
}