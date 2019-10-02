public class Boss:Monster
{
    //Is the Boss Alive?
    public bool IsAlive = true;

    //Constructor
    public Boss(string name, string followUp, pClass pClass, int xp, int gold, int addHP, int addDam, int hit, int defence, int crit, Drop drop, bool IsAlive)
    : base(name, pClass, xp, gold, addHP,addDam, hit, defence, crit,drop)
    {
        this.followUp = followUp;
        this.IsAlive = IsAlive;
    }

    public static Boss[] BossList = new Boss[]
    {
        new Boss("Savage Orc", "The Savage Orc lies dead at your feet. Your village is safe for now.\nBe wary tho, there are still more dangers on the horizon...", pClass.MonsterClassList[3], 35, 130, 5, 3, 80,10,8, Drop.BossDrop[0], true),
        new Boss("Ettin", "The Savage Orc lies dead at your feet. Your village is safe for now.\nBe wary tho, there are still more dangers on the horizon...", pClass.MonsterClassList[3], 35, 130, 0, 0, 84, 15, 12, Drop.BossDrop[1], true)
    };

    public static Boss[] MiniBossList = new Boss[]
    {
        new Boss("Head Butler", "The Savage Orc lies dead at your feet. Your village is safe for now.\nBe wary tho, there are still more dangers on the horizon...", pClass.MonsterClassList[3], 35, 130, 0, 0,80, 13, 10, Drop.BossDrop[0], true),
        new Boss("Head Cook", "The Savage Orc lies dead at your feet. Your village is safe for now.\nBe wary tho, there are still more dangers on the horizon...", pClass.MonsterClassList[3], 35, 130, 0, 0,80, 13, 10, Drop.BossDrop[1], true)
    };

}