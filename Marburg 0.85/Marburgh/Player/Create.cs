using System;

class Create
{
    public static Creature p;
    public static Family f;
    public static string[] mageAttacks = new string[] { "["+ Colour.ABILITY + "3" +Colour.RESET+ "]" + Colour.ABILITY + "Fireblast" + Colour.RESET, "[4]Arcane Missiles", "[5]Arcane Explosion", "[6]Ice Bolt" };
    public static string[] rogueAttacks = new string[] { "["+ Colour.ABILITY + "3" + Colour.RESET + "]" + Colour.ABILITY + "Stun" + Colour.RESET, "[4]Backstab", "[5]Stun", "[6]Vanish" };
    public static string[] warriorAttacks = new string[] { "["+ Colour.ABILITY + "3" + Colour.RESET + "]" + Colour.ABILITY + "Rend" + Colour.RESET, "[4]Scream" , "[5]Smash", "[6]Enrage" };
    public static pClass Warrior = new pClass("Warrior", 20, 2, 0, 0, 1);
    public static pClass Rogue = new pClass("Rogue", 16, 3, 0, 0, 2);
    public static pClass Mage = new pClass("Mage", 16, 2, 2, 2, 3);
    public static pClass[] HeroList = new pClass[] { Warrior, Rogue, Mage };

    public static void Character1Create()
    {
        Family.FamilyCreate();
        Character1Select();
        Family.FamilyAssignment();
        Marburgh.Program.Story();
        Marburgh.Program.GameTown();
    }

    public static void Character2Create()
    {
        Character2Select1();
        Family.FamilyAssignment();
        Marburgh.Program.GameTown();
    }

    public static void Character3Create()
    {
        Character3Select1();
        Family.FamilyAssignment();
        Marburgh.Program.GameTown();
    }

    public static void Character1Select()
    {
        Console.Clear();
        Console.WriteLine("Please select a class\n");
        Utilities.EmbedColourText(Colour.CLASS,Colour.CLASS, "", "[W]","arrior           ","Practiced in combat, durable and menacing.","\n");
        Utilities.EmbedColourText(Colour.CLASS,Colour.CLASS, "", "[R]","ogue             ","High Damage, Good Evasion.","\n ");
        Utilities.EmbedColourText(Colour.CLASS,Colour.CLASS, "", "[M]","age              ","Spells learned through intricate rituals, strong and versatile.","");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        f = new Family(Family.LN, Family.FamilyNames[0], Family.FamilyNames[1], Family.FamilyNames[2]);
        if (choice == "w") p = new Hero(Warrior, f, Shop.WeaponList[0], Shop.ArmorList[1], warriorAttacks);
        else if (choice == "r") p = new Hero(Rogue, f, Shop.WeaponList[1], Shop.ArmorList[0], rogueAttacks);
        else if (choice == "m") p = new Hero(Mage, f, Shop.WeaponList[0], Shop.ArmorList[0], mageAttacks);
        else Character1Select();
        CharacterSelectConfirm();
    }

    public static void Character2Select1()
    {
        Console.Clear();
        Console.WriteLine("Please select a class\n");
        Utilities.EmbedColourText(Colour.CLASS, "", "[W]arrior", "          Practiced in combat, durable and menacing.\n");
        Utilities.EmbedColourText(Colour.CLASS, "", "[R]ogue  ", "          High Damage, Good Evasion.\n ");
        Utilities.EmbedColourText(Colour.CLASS, "", "[M]age   ", "          Spells learned through intricate rituals, strong and versatile.");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        Family f = new Family(Family.LN, Family.FamilyNames[1], Family.FamilyNames[2], Family.FamilyNames[0]);
        if (choice == "w") p = new Hero(Warrior, f, Shop.WeaponList[0], Shop.ArmorList[1], warriorAttacks);
        else if (choice == "r") p = new Hero(Rogue, f, Shop.WeaponList[1], Shop.ArmorList[0], rogueAttacks);
        else if (choice == "m") p = new Hero(Mage, f, Shop.WeaponList[0], Shop.ArmorList[0], mageAttacks);
        else Character2Select1();
        CharacterSelectConfirm();
    }

    public static void Character3Select1()
    {
        Console.Clear();
        Console.WriteLine("Please select a class\n");
        Utilities.EmbedColourText(Colour.CLASS, "", "[W]arrior", "          Practiced in combat, durable and menacing.\n");
        Utilities.EmbedColourText(Colour.CLASS, "", "[R]ogue  ", "          High Damage, Good Evasion.\n ");
        Utilities.EmbedColourText(Colour.CLASS, "", "[M]age   ", "          Spells learned through intricate rituals, strong and versatile.");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        Family f = new Family(Family.LN, Family.FamilyNames[2], Family.FamilyNames[0], Family.FamilyNames[1]);
        if (choice == "w") p = new Hero(Warrior, f, Shop.WeaponList[0], Shop.ArmorList[1], warriorAttacks);
        else if (choice == "r") p = new Hero(Rogue, f, Shop.WeaponList[1], Shop.ArmorList[0], rogueAttacks);
        else if (choice == "m") p = new Hero(Mage, f, Shop.WeaponList[0], Shop.ArmorList[0], mageAttacks);
        else Character3Select1();
        CharacterSelectConfirm();
    }

    public static void CharacterSelectConfirm()
    {
        Utilities.EmbedColourText(Colour.CLASS, "\n\nYou are a ", p.pClass.cName, ", is that correct?\n\n[Y]es    [N]o");
        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (confirm == "y") return;
        CharacterSelectConfirm();
    }
}