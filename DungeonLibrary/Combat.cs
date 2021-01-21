using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Combat
    {   //will be called twice when player attacks and when the demon will attack.. dice roll could be rolled the same cuz it processes so fast
        public static void DoAttack(Character attacker, Character defender)//two values passed in...
        {
            Random rand = new Random();
            int diceRoll = rand.Next(1, 101);//range of 1 to 100
            System.Threading.Thread.Sleep(50);//milliseconds
            if (diceRoll <= attacker.CalcHitChance() - defender.CalcBlock())
            {
                //the attacker hit...
                int damageDealt = attacker.CalcDamage();

                defender.Life -= damageDealt;//says take the existing level of life and subtract damage dealt and re-assign back to life
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{attacker.Name} hit {defender.Name} for " +
                    $"{damageDealt} damage!\n");
                Console.ResetColor();


            }//end if
            else
            {
                Console.WriteLine($"{attacker.Name} missed!\n");
            }//end else
        }//end DoAttack()

        public static void DoBattle(Player player, Demons demons)
        {
            DoAttack(player, demons);
            if (demons.Life > 0)
            {
                DoAttack(demons, player);
            }//end if
        }//end DoBattle()
    }//end class
}//end namespace
