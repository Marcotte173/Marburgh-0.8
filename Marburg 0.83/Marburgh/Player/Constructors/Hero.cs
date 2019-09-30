using System.Collections.Generic;
public class Hero : Creature
{
    public Hero(pClass pClass, Family family, Equipment Weapon, Equipment Armor, string[] attacks)
    : base(pClass)
    {
        this.attacks = attacks;
        List<Drop> Drops = new List<Drop> { };
        this.family = family;
        this.pClass = pClass;
        this.Weapon = Weapon;
        this.Armor = Armor;
        energy = maxEnergy = pClass.startingEnergy;
        health = maxHealth = pClass.startingHealth;
        magic = pClass.startingMagic;
        damage = pClass.startingDamage;
        level = 1;
        gold = 100;
        xp = 0;
        potions = 1;
        bankGold = 0;
        invested = 0;
        hasInvestment = false;
        craft = false;
        maxPotions = 1;
        if (pClass.cName == "Warrior")
        {
            lvlDamage = (level == 3 || level == 5 || level == 7 || level == 10) ? 2 : 1;
            lvlHealth = (level == 3 || level == 5 || level == 7 || level == 10) ? 5 : 3;
            lvlEnergy = (level == 3 || level == 5 || level == 7 || level == 10) ? 1 : 2;
        }
        if (pClass.cName == "Rogue")
        {
            lvlDamage = (level == 3 || level == 5 || level == 7 || level == 10) ? 3 : 1;
            lvlHealth = (level == 3 || level == 5 || level == 7 || level == 10) ? 5 : 2;
            lvlEnergy = (level == 3 || level == 5 || level == 7 || level == 10) ? 2 : 2;
        }
        if (pClass.cName == "Mage")
        {
            lvlDamage = (level == 3 || level == 5 || level == 7 || level == 10) ? 2 : 1;
            lvlHealth = (level == 3 || level == 5 || level == 7 || level == 10) ? 4 : 2;
            lvlEnergy = (level == 3 || level == 5 || level == 7 || level == 10) ? 3 : 2;
            maxPotions = 2;
            Combat.currentAttackOptions[0] = Create.mageAttacks[0];
        }
    }
}