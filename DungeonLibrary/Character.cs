using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public abstract class Character
    //the abstract keyword indicates the code structure being modified is an incomplete implementation.
    //Essentially, the class is useless in the application on it's own.
    //It's only intended to pass class members to children.
    {
        private string _name;
        public int MaxLife { get; set; }
        public int HitChance { get; set; }
        public int Block { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value.Length > 0 ? value : "Unknown";
            }//end set
        }//end name


        private int _life;



        public int Life
        {
            get { return _life; }
            set
            {

                //if (value <= MaxLife)
                //{
                //    _life = value;
                //}//end if
                //else
                //{
                //    _life = MaxLife;
                //}//end else

                ////mini lab business rule into a ternary operator:

                _life = value <= MaxLife ? value : MaxLife;
            }//end set 

        }//end life

        public Character(string name, int life, int maxLife, int hitChance, int block)
        {
            Name = name;
            MaxLife = maxLife;
            Life = life;
            HitChance = hitChance;
            Block = block;

        }//end FQCTOR

        public virtual int CalcBlock()
        {
            return Block;//allows us to pass along to child classes
        }
        public virtual int CalcHitChance()
        {
            return HitChance;
        }
        public virtual int CalcDamage()
        {
            return 0;
        }

    }// end class
}//end namespace
