public class Boss:Monster
{
    //Is the Boss Alive?
    public bool IsAlive = true;

    //Constructor
    public Boss(string name, string followUp, pClass pClass, int xp, int gold, int addHP, int addDam, Drop drop, bool IsAlive)
    : base(name, pClass, xp, gold, addHP,addDam, drop)
    {
        this.followUp = followUp;
        this.IsAlive = IsAlive;
    }

    public static Boss[] BossList = new Boss[]
    {
        new Boss("Savage Orc", "The Savage Orc lies dead at your feet. Your village is safe for now.\nBe wary tho, there are still more dangers on the horizon...", pClass.MonsterClassList[3], 35, 130, 5, 3, Drop.BossDrop[0], true),
        new Boss("Ettin", "The Savage Orc lies dead at your feet. Your village is safe for now.\nBe wary tho, there are still more dangers on the horizon...", pClass.MonsterClassList[3], 35, 130, 5, 3, Drop.BossDrop[1], true)
    };

}