using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Enlil : Demons
    {

        //mod calcblock less likely to hit...
        public bool IsAncient { get; set; }


        public Enlil(string name, int life, int maxLife, int hitChance, int block, int minDamage, int maxDamage, string description, bool isAncient) : base(name, life, maxLife, hitChance, block, minDamage, maxDamage, description)
        {
            IsAncient = IsAncient;

        }//end FQCTOR
        public override int CalcBlock()
        {
            //dragon is ancient will increase block by 50%
            int calculatedBlock = Block;

            if (IsAncient)
            {
                calculatedBlock += calculatedBlock / 2;//can't increase by .5 bc we are an int. so divide by 2..
            }//end if


            return calculatedBlock;
        }//end CalcBLock
    }//end class
}
