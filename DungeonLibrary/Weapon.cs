using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Weapon
    {

        private int _minDamage;
        public int MaxDamage { get; set; }
        public string Name { get; set; }
        public int BonusHitChance { get; set; }
        public bool IsTwoHanded { get; set; }
        public int MinDamage
        {
            get { return _minDamage; }
            set
            {
                //if (value > 0 && value <= MaxDamage)
                //{
                //    _minDamage = value;
                //}//end if
                //else
                //{
                //    _minDamage = 1;
                //}//end else
                //refactoring of business rule
                _minDamage = value > 0 && value <= MaxDamage ? value : 1;

            }//end set
        }//end MinDamage
        public Weapon(string name, int minDamage, int maxDamage, int bonusHitChance, bool isTwoHanded)
        {
            MaxDamage = maxDamage;
            Name = name;
            MinDamage = minDamage;
            BonusHitChance = bonusHitChance;
            IsTwoHanded = isTwoHanded;

        }//end FQCTOR


        public Weapon()
        {
        }//end CTOR

        public override string ToString()
        {
            return string.Format("{0}\n{1} to {2} damage\nHit Modifier: {3}%\nOne or Two handed weapon: {4}",
                Name,
                MinDamage,
                MaxDamage,
                BonusHitChance,
                IsTwoHanded ? "Two" : "One");
        }//end ToString()
    }//end class
}//end namespace
