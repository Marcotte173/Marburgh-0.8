public class Drop
{
    //Variables, self explanatory
    public string name;
    public int amount;
    public int dropChance;
    public int rare;

    //Constructor
    public Drop(string name, int amount, int dropChance, int rare)
    {
        this.rare = rare;
        this.name = name;
        this.amount = amount;
        this.dropChance = dropChance;
    }

    public static Drop[] drop = new Drop[]
        {
            new Drop("Nothing",0,0,0),
            new Drop("Goblin Tooth",1,35,0),
            new Drop("Candle",1,35,0),
            new Drop("Green Goo",1,35,0)
        };

    public static Drop[] BossDrop = new Drop[]
    {
        new Drop("Savage Orc Claw", 1, 100,1),
        new Drop("Ettin Head", 1, 100,1)
    };
}