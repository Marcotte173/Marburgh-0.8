using System.Collections.Generic;

public class Dungeon
{
    //Variables, self explanatory
    public string name;
    public Boss boss;
    public Room[] roomOptions;
    public Room[] specialRoomOptions;
    public Room[] staticRoomOptions;
    public bool dungeonAvailable;
    public string flavor;
    public int monsterSummon;
    public double diminishingReturns;
    public bool roomExplored;
    public Shell[] shell;
    public int howManySpecial;
    public static List<Dungeon> AvailableDungeon = new List<Dungeon> { };    
    public int tier;

    //DUNGEON CREATION
    //Constructor
    public Dungeon(string name, int tier, string flavor,  int howManySpecial, int monsterSummon, Room[] roomOptions, Room[] specialRoomOptions, Room[] staticRoomOptions, Boss boss, bool dungeonAvailable, double diminishingReturns, bool roomExplored, Shell[] shell)
    {
        this.tier = tier;
        this.howManySpecial = howManySpecial;
        this.name = name;
        this.boss = boss;
        this.dungeonAvailable = dungeonAvailable;
        this.roomOptions = roomOptions;
        this.flavor = flavor;
        this.monsterSummon = monsterSummon;
        this.diminishingReturns = diminishingReturns;
        this.roomExplored = roomExplored;
        this.shell = shell;
        this.specialRoomOptions = specialRoomOptions;
        this.staticRoomOptions = staticRoomOptions;
    }

    public static List<Dungeon> DungeonInfo = new List<Dungeon>
    {
        new Dungeon("Starter dungeon", 1, "YOU ARE IN DUNGEON 1", 3, 60, 
        Room.StarterRoomList, Room.StarterSpecialRoomList, Room.StarterStaticRoomList,
        Boss.BossList[0], true, 1, false, Shell.StarterDungeonShell),

        new Dungeon("Castle", 1, "YOU ARE IN DUNGEON 1", 5, 65,
        Room.CastleRoomList, Room.CastleStaticRoomList, Room.CastleStaticRoomList,
        Boss.BossList[1], false, 1, false, Shell.CastleShell)
    };   

    public static void CreateDungeon(Dungeon dun)
    {        
        for (int n = 2; n < dun.shell.Length - 1; n++)
        {
            bool staticRoom = false;
            for (int m = 0; m < dun.staticRoomOptions.Length; m++)
            {
                if (dun.shell[n].assignedRoom == dun.staticRoomOptions[m])
                {
                    staticRoom = true;
                    break;
                }
            }
            if (staticRoom == false)
            {
                int roomRoll = Utilities.rand.Next(1, dun.roomOptions.Length);
                dun.shell[n].assignedRoom = dun.roomOptions[roomRoll];
            }             
        }        
        while (dun.howManySpecial > 0)
        {
            bool special = false;
            int specialRoomRoll = Utilities.rand.Next(3, dun.shell.Length-1);
            for (int x = 0; x < dun.specialRoomOptions.Length; x++)
            {
                if (dun.shell[specialRoomRoll].assignedRoom == dun.specialRoomOptions[x])
                {
                    special = true;
                    break;
                }
            }
            if (special == false)
            {
                for (int i = 0; i < dun.staticRoomOptions.Length; i++)
                {
                    if (dun.shell[specialRoomRoll].assignedRoom == dun.staticRoomOptions[i])
                    {
                        special = true;
                        break;
                    }
                }
            }
            if (special == false)
            {
                int specialRoll = Utilities.rand.Next(1, dun.specialRoomOptions.Length);
                dun.shell[specialRoomRoll].assignedRoom = dun.specialRoomOptions[specialRoll];
                dun.howManySpecial--;
            }
            if (dun.howManySpecial == 0) break;
        }
        AvailableDungeon.Add(dun);
    }      
}