using System;

public class Bank
{
    public static double bankRate = 0.05;

    public static void GameBank(Creature p)
    {
        Console.Clear();
        Console.WriteLine("You enter a small but busy bank. One teller appears to be free. You walk up to him");
        Utilities.ColourText(Colour.SPEAK, "\n'Hello. How may I be of service?'");
        Console.WriteLine("\n\n[D]eposit        [W]ithdraw");
        string a = (p.hasInvestment) ? "[R]eturn to town\n\n" : "[I]nvest         [R]eturn to town\n\n";
        Console.WriteLine(a);
        string b = "";
        string c = "";
        if (p.hasInvestment)
        {
            b = "         Investments: ";
            c = $"{p.invested}";
        }
        Utilities.EmbedColourText(Colour.GOLD, Colour.GOLD, Colour.GOLD, "Gold: ", $"{p.gold}", "         Gold in bank: ", $"{Marburgh.Program.bankGold}", $"{b}",$"{c}",$"");
        Console.WriteLine("");
        Utilities.EmbedColourText(Colour.GOLD, "The current investment rate is ", $"{bankRate}", "");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "d")
        {
            if (p.gold > 0)
            {
                int deposit;
                do
                {
                    Utilities.ColourText(Colour.SPEAK, "\n'Excellent! How much would you like to deposit?'");
                    Console.WriteLine("\n\n[0] Return \n" + Colour.GOLD);                    
                } while (!int.TryParse(Console.ReadLine(), out deposit));
                Console.WriteLine(Colour.RESET);
                if (p.gold >= deposit)
                {
                    p.gold -= deposit;
                    Marburgh.Program.bankGold += deposit;
                    Utilities.EmbedColourText(Colour.GOLD, "\nYou deposit ", $"{deposit} ", "gold.");
                }
                else Utilities.ColourText(Colour.SPEAK, "\n'You don't have enough money!'");
            }
            else Utilities.ColourText(Colour.SPEAK, "\n'You don't have any money!'");
            Utilities.Keypress();
        }
        else if (choice == "w")
        {
            if (Marburgh.Program.bankGold > 0)
            {
                int withdraw;
                do
                {
                    Utilities.ColourText(Colour.SPEAK, "\n'Excellent! How much would you like to withdraw?'");
                    Console.WriteLine("\n\n[0] Return \n" + Colour.GOLD);
                } while (!int.TryParse(Console.ReadLine(), out withdraw));
                Console.WriteLine(Colour.RESET);
                if (Marburgh.Program.bankGold >= withdraw)
                {
                    p.gold += withdraw;
                    Marburgh.Program.bankGold -= withdraw;
                    Utilities.EmbedColourText(Colour.GOLD, "\nYou withdraw ", $"{withdraw} ", "gold.");
                }
                else Utilities.ColourText(Colour.SPEAK, "\n'You don't have enough money in the bank!'");
            }
            else Utilities.ColourText(Colour.SPEAK, "\n'You don't have any money in the bank!'");
            Utilities.Keypress();
        }
        else if (choice == "i" && p.hasInvestment == false)
        {
            if (p.gold > 0)
            {
                int invest;
                do
                {
                    Utilities.ColourText(Colour.SPEAK, "\n'Excellent! How much would you like to Invest?'");
                    Console.WriteLine("\n\n[0] Return \n" + Colour.GOLD);
                } while (!int.TryParse(Console.ReadLine(), out invest));
                Console.WriteLine(Colour.RESET);
                if (p.gold >= invest && invest > 0)
                {
                    p.gold -= invest;
                    p.invested = invest;
                    Utilities.EmbedColourText(Colour.GOLD, "\nYou invest ", $"{invest} ", "gold.");
                    p.investmentTerm = 3;
                    p.hasInvestment = true;
                }
                else if (invest == 0) GameBank(p);
                else Utilities.ColourText(Colour.SPEAK, "\n'You don't have enough money!'");
            }
            else Utilities.ColourText(Colour.SPEAK, "\n'You don't have any money!'");
            Utilities.Keypress();
        }
        else if (choice == "r") Marburgh.Program.GameTown();
        GameBank(p);
    }

    public static void InvestCheck()
    {
        if (Create.p.hasInvestment && Create.p.investmentTerm <= 0)
        {
            double br = Convert.ToDouble(Create.p.invested);
            br += br * bankRate;
            Create.p.invested = Convert.ToInt32(br);
            Console.Clear();
            Create.p.hasInvestment = false;
            Utilities.EmbedColourText(Colour.GOLD, "Your investments have paid off! You receive ", $"{Create.p.invested}", " gold.\n");
            Create.p.gold += Create.p.invested;
            Utilities.Keypress();
        }
    }
}