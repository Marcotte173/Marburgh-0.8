using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CastleEvent
{
    public static void Room(Dungeon d, Creature p, Event[] Event)
    {
        foreach (Event e in Event)
        {
            int a = e.eventType;
            switch (a)
            {
                case 1:
                    Event1(d, p, e);
                    break;
                case 2:
                    Event2(d, p, e);
                    break;
                case 3:
                    Event3(d, p, e);
                    break;
                case 4:
                    Event4(d, p, e);
                    break;
                case 5:
                    Event5(d, p, e);
                    break;
                case 6:
                    Event6(d, p, e);
                    break;
            }
        }
    }

    public static void Event1(Dungeon d, Creature p, Event e)
    {
        
    }

    public static void Event2(Dungeon d, Creature p, Event e)
    {
        
    }

    public static void Event3(Dungeon d, Creature p, Event e)
    {
        
    }

    public static void Event4(Dungeon d, Creature p, Event e)
    {
        
    }
    public static void Event5(Dungeon d, Creature p, Event e)
    {

    }
    public static void Event6(Dungeon d, Creature p, Event e)
    {

    }
}