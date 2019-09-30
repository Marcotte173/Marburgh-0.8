public class DayEvent
{
    public string name;
    public bool gameOver;
    public string flavor;
    public int day;
    public int week;
    public int month;
    public int year;
    public bool active;
    public bool trigger;

    public DayEvent(string name, bool active, bool trigger,  bool gameOver, string flavor, int day, int week, int month, int year)
    {
        this.name = name;
        this.flavor = flavor;
        this.day = day;
        this.week = week;
        this.month = month;
        this.year = year;
        this.active = active;
        this.gameOver = gameOver;
        this.trigger = trigger;
    }
}