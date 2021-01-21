using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Demons : Character
    {
        private int _minDamage;//'propfull' tab tab to get this layout... Demons is same a weapon
        public int MaxDamage { get; set; }
        public string Description { get; set; }

        public int MinDamage
        {
            get { return _minDamage; }
            set
            {
                _minDamage = value > 0 && value <= MaxDamage ? value : 1;
            }//end set
        }//end MinDamage

        public Demons(string name, int life, int maxLife, int hitChance, int block, int minDamage, int maxDamage, string description) : base(name, life, maxLife, hitChance, block)
        {
            MaxDamage = maxDamage;//has to be set before min damage due to business rule!!!!
            MinDamage = minDamage;
            Description = description;
        }//end FQCTOR

        public override string ToString()
        {
            return string.Format($"{Name}\n{Description}\nLife Remaining: {Life} of {MaxLife}\nHit Chance: {HitChance}\nBlock Ability: {Block}\nDamage Range: {MinDamage} to {MaxDamage}\n");

        }//end ToString()

        public override int CalcDamage()
        {
            return new Random().Next(MinDamage, MaxDamage + 1);
        }//end CalcDamage()

    }//end class Demons
}
