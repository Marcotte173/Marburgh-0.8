using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Room
{
    //Variables, self explanatory
    public string name;
    public string[] flavor;
    public int modifier;
    public Event[] EventArray;
    public static List<Event> EventDisplay = new List<Event> { };
    public Monster[] bestiary ;

    //Constructor
    public Room(string name, string[] flavor, int modifier, Event[] EventArray, Monster[] bestiary)
    {
        this.name = name;
        this.flavor = flavor;
        this.modifier = modifier;
        this.EventArray = EventArray;
        this.bestiary = bestiary;
    }

    public static Room[] StarterRoomList = new Room[]
    {
            new Room("Entrance", new string[]{"You are at the entrance to the dungeon","You are at the entrance to the dungeon" },0,new Event[] {Event.SearchList[0] }, Monster.starterBestiary),
            new Room("Small room", new string[]{"You enter a small, squalid room","You have explored this small room" }, 5, new Event[] { Event.SearchList[1] },Monster.starterBestiary),
            new Room("Small library",new string[]{"You enter a small squalid library", "You have explored this library"},5, new Event[]{ Event.SearchList[1], Event.SearchList[3]},Monster.starterBestiary),
            new Room("Small room", new string[]{"You enter a smallish, squalid room","You have explored this room"}, 5, new Event[]{ Event.SearchList[1], Event.SearchList[2]},Monster.starterBestiary),
    };

    public static Room[] StarterSpecialRoomList = new Room[]
    {
            new Room("Plundered loot",new string[]{"A room with a treasure chest!","You see an old empty chest" },0,new Event[] { Event.StarterSpecialSearchList[2] },null),
            new Room("Shamanistic rune",new string[]{"A room with a small altar"," You see a broken altar" },0,new Event[] { Event.StarterSpecialSearchList[3] },null),
            new Room("Elite Orc",new string[]{"An elite Orc!","You see an empty room with a dead ogre in it" },0,new Event[] { Event.StarterSpecialSearchList[4] },Monster.starterBestiary)
    };

    public static Room[] StarterStaticRoomList = new Room[]
    {            
            new Room("Boss Room", new string[]{$"A secret Lair!","A secret Lair!"},0,new Event[] { Event.StarterSpecialSearchList[0] },null),
            new Room("Captive townsfolk",new string[]{"A makeshift prison cell","You see an old prison. Thank goodness the people escaped" },0,new Event[] { Event.StarterSpecialSearchList[1] },null)
    };

    public static Room[] CastleRoomList = new Room[]
    {
            new Room("Entrance", new string[]{"You are at the entrance to the castle","You are at the entrance to the dungeon" },0,new Event[] {Event.SearchList[0] },Monster.castleBestiary),
            new Room("Medium Room",new string[]{"You enter a medium sized, regular room","You have explored this medium room"}, 10,new Event[]{ Event.SearchList[1] },Monster.castleBestiary),
            new Room("Medium Room", new string[]{"You enter a medium sized, regular room","You have explored this medium room"}, 10,new Event[]{ Event.SearchList[1], Event.SearchList[2], Event.SearchList[3]},Monster.castleBestiary),
            new Room("Servants quarters", new string[]{"You enter what appear to be the servant's quarters","You have explored this room"}, 10,new Event[]{ Event.SearchList[1], Event.SearchList[2], Event.SearchList[3]},Monster.castleBestiary)
    };

    public static Room[] CastleSpecialRoomList = new Room[]
    {
            new Room("Boss Room", new string[]{$"A secret Lair!","A secret Lair!"},0,new Event[] { Event.StarterSpecialSearchList[0] },null),
            new Room("Captive townsfolk",new string[]{"A makeshift prison cell","You see an old prison. Thank goodness the people escaped" },0,new Event[] { Event.StarterSpecialSearchList[1] },null),
            new Room("Plundered loot",new string[]{"A room with a treasure chest!","You see an old empty chest" },0,new Event[] { Event.StarterSpecialSearchList[2] },null),
            new Room("Shamanistic rune",new string[]{"A room with a small altar"," You see a broken altar" },0,new Event[] { Event.StarterSpecialSearchList[3] },null),
            new Room("Elite Orc",new string[]{"An elite Orc!","You see an empty room with a dead ogre in it" },0,new Event[] { Event.StarterSpecialSearchList[4] },Monster.starterBestiary)
    };        

    public static Room[] CastleStaticRoomList = new Room[]
    {
            new Room("Boss Room", new string[]{$"A secret Lair!","A secret Lair!"},0,new Event[] { Event.StarterSpecialSearchList[0] },null),
            new Room("Captive townsfolk",new string[]{"A makeshift prison cell","You see an old prison. Thank goodness the people escaped" },0,new Event[] { Event.StarterSpecialSearchList[1] },Monster.castleBestiary),
            new Room("Plundered loot",new string[]{"A room with a treasure chest!","You see an old empty chest" },0,new Event[] { Event.StarterSpecialSearchList[2] },Monster.castleBestiary),
            new Room("Shamanistic rune",new string[]{"A room with a small altar"," You see a broken altar" },0,new Event[] { Event.StarterSpecialSearchList[3] },Monster.castleBestiary),
            new Room("Elite Orc",new string[]{"An elite Orc!","You see an empty room with a dead ogre in it" },0,new Event[] { Event.StarterSpecialSearchList[4] },Monster.castleBestiary),
            new Room("Elite Orc",new string[]{"An elite Orc!","You see an empty room with a dead ogre in it" },0,new Event[] { Event.StarterSpecialSearchList[4] },Monster.castleBestiary)
    };

    public static void RegularRoom(Dungeon d, Creature p, Shell currentShell)
    {
        //Check for monsters
        Monster.Summon(currentShell.assignedRoom, d, p);
        //Get a chance to explore
        RoomSearch(currentShell.assignedRoom, d, p);
        //If you chose not to explore it's back to main dungeon screen, this second chance at baddies is risk for searching      
        //You may have been heard
        Console.Clear();
        Console.Write("You hear shuffling in the distance, were you heard?");
        Utilities.DotDotDot();
        Monster.Summon(currentShell.assignedRoom, d, p);
        if (d.monsterSummon > 50) Console.WriteLine("Phew! You got luckey!");
        else Console.WriteLine("No one came to investigate.\nNot surprising, the dungeon seems pretty empty");
        Utilities.Keypress();
        Explore.currentShell.encountered = true;
        Explore.GameDungeon(d, p);
        
    }

    public static void RoomSearch(Room room, Dungeon d, Creature p)
    {
        Console.Clear();
        //Search or move on. Search can get stuff but risks a fight if you didn't have one yet.
        Console.WriteLine("You appear to be alone... for now");
        Console.WriteLine("You can either [S]earch the room or [M]ove on");
        Console.WriteLine("\nWhat would you like to do?");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "m")
        {
            Explore.currentShell.encountered = true;
            Explore.GameDungeon(d, p);
        }

        if (choice == "s")
        {
            //Make a list for events, only add if successful
            EventDisplay = new List<Event> { };
            Console.Clear();
            Console.Write("You find");
            Utilities.DotDotDotSL();
            Console.Write(" ");
            for (int i = 0; i < room.EventArray.Length; i++)
            {
                //if successful, add event to list
                int EventSuccessRoll = Utilities.rand.Next(1, 101);
                if (EventSuccessRoll <= room.EventArray[i].success)
                {
                    EventDisplay.Add(room.EventArray[i]);
                }
            }
            //Tell us what we won!
            string a = (EventDisplay.Count == 3) ? $"{EventDisplay[0].flavor},{EventDisplay[1].flavor} and {EventDisplay[2].flavor}" : (EventDisplay.Count == 2) ? $"{EventDisplay[0].flavor} and {EventDisplay[1].flavor}" : (EventDisplay.Count == 1) ? $"{EventDisplay[0].flavor}" : "Nothing!";
            Console.WriteLine(a);
            Console.WriteLine("");
            // Now give it to me!
            Reward.RoomSearch(room, d, p, EventDisplay, d.tier);     
            Utilities.Keypress();
        }
        else RoomSearch(room, d, p);
    }

    public static void SpecialRoom(Dungeon d, Creature p, Event[] Event)
    {
        foreach (Event e in Event)
        {
            int a = e.eventType;
            switch (a)
            {
                case 1:
                    SpecialEvent.Event1(d, p, e);
                    break;
                case 2:
                    SpecialEvent.Event2(d, p, e);
                    break;
                case 3:
                    SpecialEvent.Event3(d, p, e);
                    break;
                case 4:
                    SpecialEvent.Event4(d, p, e);
                    break;
            }
        }
    }
}