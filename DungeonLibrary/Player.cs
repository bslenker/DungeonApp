using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Player : Character
    {


        public Race HeroRace { get; set; }
        public Weapon EquippedWeapon { get; set; }
        

        public Player(string name, int life, int maxLife, int hitChance, int block, Race heroRace, Weapon equippedWeapon) : base(name, life, maxLife, hitChance, block)
        {
            HeroRace = heroRace;
            EquippedWeapon = equippedWeapon;

            switch (HeroRace)//modifying the default set values in the switch
            {
                case Race.Human:
                    HitChance += 1;
                    Life -= 5;
                    MaxLife -= 5;
                    break;
                case Race.Elf:
                    HitChance += 10;
                    Life -= 5;
                    MaxLife -= 5;
                    break;
                case Race.Dwarf:
                    HitChance += 7;
                    Life -= 4;
                    MaxLife -= 4;
                    break;
                case Race.Magi:
                    Block += 6;
                    Life += 2;
                    MaxLife += 3;
                    break;
                case Race.StarSeed:
                    HitChance -= 1;
                    MaxLife += 5;
                    Life += 5;
                    break;

            }
            
        }//end FQCTOR
        public override string ToString()
        {
            return string.Format("******** {0} *********\nLife: {1} of {2}\n" +
                "HitChance: {3}%\nBlock: {4}%\nRace: {5}\nWeapon: {6}",
                Name,
                Life,
                MaxLife,
                CalcHitChance(),
                Block,
                HeroRace,
                EquippedWeapon);

        }//end ToString()

        public override int CalcDamage()
        {
            //create an int value and return it
            //Random rand = new Random();
            //int damage = rand.Next(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage +1);
            //return damage;
            //refactored from above to below one line
            return new Random().Next(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage + 1);


        }//end CalcDamage

        public override int CalcHitChance()
        {
            return HitChance + EquippedWeapon.BonusHitChance;

        }//end CalcHitChance()

    }//end class
}//end namespace
