using System;

public class Event
{
    //Variables, self explanatory
    public string name;
    public string flavor;
    public int eventType;
    public int effect;
    public int success;
    public static Random rand = new Random();
    public static bool TFRescued = false;

    //Constructor
    public Event(string name, string flavor, int eventType, int effect, int success)
    {
        this.name = name;
        this.flavor = flavor;
        this.eventType = eventType;
        this.effect = effect;
        this.success = success;
    }

    public static Event[] SearchList = new Event[]
    {
            new Event("First room", "", 0,0,0),
            new Event("Gold",  "Some" + Colour.GOLD + " gold" + Colour.RESET,        0 ,     60,           50),
            new Event("Potion","A "+ Colour.HEALTH +"potion" + Colour.RESET,  0,      1,             40),
            new Event("XP", "An old " + Colour.XP + "book" + Colour.RESET,         0,      10,            30)
    };

    public static Event[] StarterSpecialSearchList = new Event[]
    {
            new Event("Boss Room","",0,1,1),
            new Event("Captive townsfolk", "You see several townsfolk huddling for warmth, obviously scared.\nOne comes up to you to speak\n\n"+ Colour.SPEAK + "'Thank god you're here! We'd given up all hope!" +
                "\nUntie us and we will reward you handomely when we get back!'\n\n"+ Colour.RESET + "You untie them and point the way out.\n" +
                "Be sure to meet them in the tavern afterwards to claim your reward.\n\nThat is.... if you live",
                1,      20,            40),
            new Event("Plundered Loot", "A large chest of goods with a sturdy lock sits here. " +
                "\n\nThe goblins haven't managed to bypass the lock yet, but an old goblin dagger sticks out of the top of the chest" +
                "\nwhere they attempted to create a new opening.\n",         2,      20,            40),
            new Event("Shamanistic rune", "You approach the altar and find a rune, placed with great care and reverance. " +
                "\n\nYou recognize it as belonging to one of the Orc gods, who they belive bring them strength and victory." +
                "\nThey would be angry indeed if anything were to happen to it.\n",         3,      20,            40),
            new Event("Elite Orc", "One of the savage orc's personal guards have found you!/nYou have no choice but to fight him",
                4,      20,            40)        
    };
}