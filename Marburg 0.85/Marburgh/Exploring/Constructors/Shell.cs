public class Shell
{
    public int North;
    public int South;
    public int East;
    public int West;
    public string flavor;
    public bool current;
    public Room assignedRoom;
    public bool visited;
    public bool encountered;


    public Shell(int North, int South, int East, int West, string flavor, bool current, bool visited, bool encountered, Room assignedRoom)
    {
        this.North = North;
        this.South = South;
        this.East = East;
        this.West = West;
        this.flavor = flavor;
        this.current = current;
        this.assignedRoom = assignedRoom;
        this.visited = visited;
        this.encountered = encountered;
    }

    public static Shell[] StarterDungeonShell = new Shell[]
    {
        new Shell(0,0,0,0,"",false, false,false, Room.StarterRoomList[0]),
        new Shell(2,0,0,0,"Entrance",true, true, true, Room.StarterRoomList[0]),
        new Shell(4,1,3,0,"Room One",false,false,  false, Room.StarterRoomList[0]),
        new Shell(7,0,0,2, "Room Two",false,false, false, Room.StarterStaticRoomList[1]),
        new Shell(0,2,0,5,"Room Three",false,false,false, Room.StarterRoomList[0]),
        new Shell(6,0,4,0,"Room Four",false,false, false, Room.StarterRoomList[0]),
        new Shell(0,5,9,0, "Room Five",false,false,false, Room.StarterRoomList[0]),
        new Shell(8,3,0,0, "Room Six",false,false,false, Room.StarterRoomList[0]),
        new Shell(0,7,0,9, "Room Seven",false,false,false, Room.StarterRoomList[0]),
        new Shell(0,0,8,6, "Boss Room",false, false,false, Room.StarterStaticRoomList[0])
    };

    public static Shell[] CastleShell = new Shell[]
    {
        new Shell(0,0,0,0,"",false, false,false, Room.StarterRoomList[0]),

        new Shell(2,0,0,0,"Entrance",true, true, true, Room.CastleRoomList[0]),
        new Shell(8,1,14,3,"Room One",false,false,  false, Room.CastleRoomList[0]),
        new Shell(0,4,2,0, "Room Two",false,false, false, Room.CastleStaticRoomList[3]),
        new Shell(3,0,0,5,"Room Three",false,false,false, Room.CastleRoomList[0]),
        new Shell(6,0,4,7,"Room Four",false,false, false, Room.CastleRoomList[0]),
        new Shell(0,5,0,0, "Room Five",false,false,false, Room.CastleRoomList[0]),
        new Shell(0,0,5,0, "Room Six",false,false,false, Room.CastleStaticRoomList[1]),
        new Shell(0,2,9,0, "Room Seven",false,false,false, Room.CastleStaticRoomList[4]),
        new Shell(10,0,0,8, "Boss Room",false, false,false, Room.CastleRoomList[0]),
        new Shell(0,9,0,11,"Entrance",true, true, true, Room.CastleRoomList[0]),

        new Shell(0,0,10,0,"Room One",false,false,  false,  Room.CastleStaticRoomList[2]),
        new Shell(13,0,8,0, "Room Two",false,false, false,  Room.CastleRoomList[0]),
        new Shell(0,12,0,0,"Room Three",false,false,false,  Room.CastleRoomList[0]),
        new Shell(0,0,15,2,"Room Four",false,false, false,  Room.CastleStaticRoomList[5]),
        new Shell(19,16,18,14, "Room Five",false,false,false,  Room.CastleRoomList[0]),
        new Shell(15,0,17,0, "Room Six",false,false,false,   Room.CastleRoomList[0]),
        new Shell(18,0,0,16, "Room Seven",false,false,false, Room.CastleRoomList[0]),
        new Shell(0,17,0,15, "Boss Room",false, false,false, Room.CastleRoomList[0]),
        new Shell(0,15,20,0,"Entrance",true, true, true,     Room.CastleRoomList[0]),
        new Shell(0,0,21,19,"Room One",false,false,  false,  Room.CastleRoomList[0]),

        new Shell(0,22,0,20, "Room Two",false,false, false, Room.CastleRoomList[0]),
        new Shell(21,0,0,0,"Room Three",false,false,false, Room.CastleStaticRoomList[0])
    };
}