using DungeonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;



namespace Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -1;     // -10...10

            // Synchronous
            synthesizer.Speak("Would you like to play a game?");

            // Asynchronous
            //synthesizer.SpeakAsync("texttt");


            Console.WriteLine("Welcome to the deep, dark depths of Enlil's underworld.");
            synthesizer.SpeakAsync("Welcome to the deep, dark depths of Enlil's underworld.");
            Console.Title = "Welcome to the Dungeon of Enlil!";

            int score = 0;
            synthesizer.SpeakAsync("Please enter your name!");
            Console.WriteLine("Please enter your name!");
            
            string heroName = Console.ReadLine();//player gets to enter their name...

            Race heroRace;

            synthesizer.Speak("Please choose your race!");
            Console.WriteLine("Choose your race:\n" +
                "H)uman (basic galactic misfit, but hardy)\n" +
                "E)lf (sharp ears to match their tongue, which gets them in trouble)\n" +
                "D)warf (small and tough, but slow with a chip on their shoulder)\n" +
                "M)agi (powerful old warriors from a forgotten time)\n" +
                "S)tarSeed (chosen ones to return to Earth's realm to battle evil)\n");
            ConsoleKey raceChoice = Console.ReadKey().Key;

            switch (raceChoice)
            {
                case ConsoleKey.H:
                    heroRace = Race.Human;
                    break;
                case ConsoleKey.E:
                    heroRace = Race.Elf;
                    break;
                case ConsoleKey.D:
                    heroRace = Race.Dwarf;
                    break;
                case ConsoleKey.M:
                    heroRace = Race.Magi;
                    break;
                case ConsoleKey.S:
                    heroRace = Race.StarSeed;
                    break;
                default:
                    synthesizer.Speak("That was an invalid choice. You are now a human..way to go.");
                    Console.WriteLine("That was an invalid choice. You are now a human..way to go.\n");
                    heroRace = Race.Human;
                    break;
            }//end switch


            synthesizer.Speak("Please choose a weapon.");
            Console.WriteLine("Choose your weapon:\n" +
            "S)cimitar (Sword of the Ancestors, single handed with great slashing damage at close range)\n" +
            "W)ar Staff (Staff of Andromeda, a gift to humans long ago with great reach with two handed use)\n" +
            "B)attleAxe (Mighty Ax of InnerEarth, close quarters Battleaxe, suffers big damage...when you actually hit something)\n" +
            "L)ong Bow and arrow (Elvish Bow of Elderrohan, good range of damage with high hit probability, two handed)\n");

            ConsoleKey weaponInput = Console.ReadKey().Key;

            Weapon heroWeapon = new Weapon();
            switch (weaponInput)
            {   
                case ConsoleKey.S:
                    heroWeapon = new Weapon("Sword of the Ancestors", 1, 6, 5, false);
                    break;
                case ConsoleKey.W:
                    heroWeapon = new Weapon("Staff of Andromeda", 2, 5, 5, true);
                    break;
                case ConsoleKey.B:
                    heroWeapon = new Weapon("Mighty Ax of InnerEarth", 2, 6, 3, true);
                    break;
                case ConsoleKey.L:
                    heroWeapon = new Weapon("Elvish Bow of Elderrohan", 3, 6, 7, true);
                    break;
                default:
                    synthesizer.Speak("That was an invalid choice. You now get the Rusty Dagger of Meeseeks...way to go.");
                    Console.WriteLine("That was an invalid choice. You now get the Rusty Dagger of Meeseeks...way to go.\n");
                    heroWeapon = new Weapon("Rusty Dagger of Meeseeks", 1, 1, 0, false);
                    break;
            }//end switch



            Player player = new Player(heroName, 40, 40, 70, 10, heroRace,
                heroWeapon);
            Console.Clear();
            Console.WriteLine($"Welcome, {player.Name} the {player.HeroRace}!\n" +
                $"\nYour journey begins!\n");
            //like a get room we could through in a dungeon room description
            bool exit = false;

            do
            {
                //if you want a collection of monsters have a type list on the outside of the do while to kill off as you go...
                Demons skeleton = new Demons("Skeleton", 10, 10, 45, 5, 1, 4, "A roaming soulless footman of bones from the depths of Enlil's lair.");
                Demons reptilian = new Demons("Reptilian", 20, 20, 50, 4, 5, 4, "Fierce, flesh hungry Reptile");
                Demons nephilim = new Demons("Nephilim", 15, 15, 50, 3, 5, 7, "Ancient giants of the old realm; half God, half human");
                Enlil boss = new Enlil("Enlil", 40, 40, 70, 10, 2, 8, "This is the ruler of the Underworld!!", true);

                Demons[] demon =//going to see polymorphism going on in here... can use child class obj with parent class obj
                {
                    boss, reptilian, reptilian, reptilian, skeleton, skeleton, skeleton, skeleton, skeleton, skeleton, nephilim, nephilim, nephilim
                };

                Demons demons = demon[new Random().Next(demon.Length)];

                Console.WriteLine(GetRoom());

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"in this room you see a {demons.Name}!");
                Console.ResetColor();



                bool reloadRoom = false;

                do
                {
                    //synthesizer.Speak("Please choose an action");
                    Console.Write("\nPlease choose an action:\n" +
                        "A)ttack\n" +
                        "F)lee\n" +
                        "H)ero Power\n" +
                        "D)emon Stats\n" +
                        "\nPress Esc to exit game");
                    ConsoleKey userChoice = Console.ReadKey().Key;

                    Console.Clear();

                    switch (userChoice)
                    {
                        case ConsoleKey.A:
                            Combat.DoBattle(player, demons);
                            if (demons.Life <= 0)//demons.life property is less than or = to 0 demons has died//healing goes here too
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine($"\nYou killed {demons.Name}!\n");
                                Console.ResetColor();
                                reloadRoom = true;
                                score++;
                            }//end if demon is dead                            

                            break;

                        case ConsoleKey.F:
                            Console.WriteLine("FLEE!\n");
                            Console.WriteLine($"{demons.Name} attacks you as you flee!");
                            Combat.DoAttack(demons, player);
                            reloadRoom = true;
                            break;

                        case ConsoleKey.H:
                            Console.WriteLine(player);
                            //players functionality
                            break;

                        case ConsoleKey.D:
                            Console.WriteLine(demons);
                            //demons functionality
                            break;

                        case ConsoleKey.Escape:
                            synthesizer.Speak("Do or do not... There is no try.\n");
                            Console.WriteLine("Do or do not... There is no try.\n");
                            exit = true;

                            break;
                        default:
                            Console.WriteLine(userChoice + " was a poor choice. Choose again.\n");
                            break;
                    }//end switch
                     //check if hero was alive
                    if (player.Life <= 0)
                    {
                        Console.WriteLine("You are now recycled into another life ");
                        exit = true;//we make both of these false and exit the game
                    }//end if
                } while (!reloadRoom && !exit);//will take you out of both loops
            } while (!exit);
            Console.WriteLine($"You slayed {score} total Demons!");
            synthesizer.Speak("Game over, goodbye!");
            Console.WriteLine("Game Over, Goodbye!");
        }//end Main()

        private static string GetRoom()
        {
            //collection initialization syntax
            string[] rooms =
            {//Room descriptions... have 10 now
                "A massive fallen tower in a gloomy marsh marks the entrance to this dungeon. Beyond the fallen tower lies a massive, dark room. It's covered in broken pottery, dirt and cobwebs.  Your torch allows you to see broken mining equipment, pillaged and defaced by time itself. Further ahead are three paths, you take the left. Its twisted trail leads passed collapsed rooms and pillaged treasuries and soon you enter a dank area. It's packed with boxes full of runes and magical equipment, as well as skeletons. What happened in this place? You slowly march onwards, deeper into the dungeon's darkness. You pass many different passages, most of which are far too ominous looking to try out. You eventually make it to what is likely the final room. An immense granite door blocks your path. Strange writing is all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. did the door just change its appearance?\n",
                "A modest waterfall in a shadowy grove marks the entrance to this dungeon. Beyond the waterfall lies a modest, shady room. It's covered in roots, bat droppings and cobwebs.  Your torch allows you to see drawings and symbols on the walls, long lost and mutilated by time itself.  Further ahead is a single path. Its twisted trail leads passed several empty rooms and soon you enter a filthy area. The room is filled with lifelike statues of terrified people. What happened in this place? You slowly move onwards, deeper into the dungeon's darkness. You pass a few more rooms and passages, each leading to who knows where, or what. You eventually make it to what is likely the final room. An enormous granite door blocks your path. Large claw marks are all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. did the door just change its appearance?\n",
                "A wide pair of worn statues in a foggy boulder field marks the entrance to this dungeon. Beyond the pair of worn statues lies a narrow, dank room. It's covered in large bones, dirt and puddles of water. Your torch allows you to see empty shelves and broken pots, destroyed and ravished by time itself. Further ahead are two paths, but the right is a dead end. Its twisted trail leads passed pillaged rooms and soon you enter a shady area. Several stacks of gunpowder barrels are stacked against a wall. A skeleton holding a torch lies before it. What happened in this place? You press onwards, deeper into the dungeon's darkness. You pass many different passages, most lead to nowhere or back to this same path. You eventually make it to what is likely the final room. An enormous granite door blocks your path. Dried blood splatters are all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. light's coming through the gap below the door.\n",
                "A grand boulder in a overcast mountain range marks the entrance to this dungeon. Beyond the boulder lies a grand, ragged room. It's covered in crawling insects, puddles of water and cobwebs. Your torch allows you to see weapons racks and locked crates, long lost and claimed by time itself. Further ahead are two paths, you take the left. Its twisted trail leads passed long lost rooms and tombs and soon you enter a ghostly area. Several stacks of gunpowder barrels are stacked against a wall. A skeleton holding a torch lies before it. What happened in this place? You advance carefully onwards, deeper into the dungeon's mysteries. You pass dozens of similar rooms and passages, most of which are far too ominous looking to try out. You eventually make it to what is likely the final room. A wide wooden door blocks your path. Strange writing is all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. you hear a faint laugh coming from behind the door.\n",
                "A wide boulder in a overcast swamp marks the entrance to this dungeon. Beyond the boulder lies a large, dusty room. It's covered in cobwebs, dirt and ash. Your torch allows you to see broken vats and flasks, pillaged and defaced by time itself. Further ahead are two paths, but the right is a dead end. Its twisted trail leads passed countless other pathways and soon you enter a dusty area. The floor is riddled with shredded blue prints and a half finished machine sits in a corner. What happened in this place? You tread onwards, deeper into the dungeon's depths. You pass a few more passages, some look awfully familiar, others stranger everything else. You eventually make it to what is likely the final room. A grand granite door blocks your path. Various odd symbols are all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. something just grabbed your shoulder.\n",
                "A grand crypt in a dark marsh marks the entrance to this dungeon. Beyond the crypt lies a massive, putrid room. It's covered in dead insects, broken pottery and moss. Your torch allows you to see remnants of what once must've been a mess hall of sorts, pillaged and ravaged by time itself. Further ahead are three paths, you take the left. Its twisted trail leads passed collapsed rooms and pillaged treasuries and soon you enter a humid area. An enormous beastly skeleton is chained to the walls. What happened in this place? You press onwards, deeper into the dungeon's depths. You pass various passages, most of which probably lead to other depths of this dungeon. You eventually make it to what is likely the final room. A massive metal door blocks your path. Dried blood splatters are all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. you hear the faint sound of footsteps behind you.\n",
                "A short worn statue in a murky morass marks the entrance to this dungeon. Beyond the worn statue lies a narrow, crumbling room. It's covered in broken pottery, ash and broken stone. Your torch allows you to see broken arrows, rusty swords and skeletal remains, pillaged and taken by time itself. Further ahead are two paths, but the right is a dead end. Its twisted trail leads passed broken and pillaged tombs and soon you enter a dank area. Piles and piles of gold lie in the center, several skeletons lie next to it. What happened in this place? You continue onwards, deeper into the dungeon's secrets. You pass countless passages, some look awfully familiar, others stranger everything else. You eventually make it to what is likely the final room. A big granite door blocks your path. Strange writing is all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. light's coming through the gap below the door.\n",
                " A minor worn statue in a dire marsh marks the entrance to this dungeon. Beyond the worn statue lies a small, filthy room. It's covered in crawling insects, dead insects and dirt. Your torch allows you to see a pillaged treasury, decayed and spoiled by time itself. Further ahead are two paths, you take the right. Its twisted trail leads passed lost treasuries, unknown rooms and armories and soon you enter a humid area. There's a pile of skeletons in the center, all burned and black. What happened in this place? You continue onwards, deeper into the dungeon's mysteries. You pass many rooms and passages, some of them have collapsed, others seem to go on forever. You eventually make it to what is likely the final room. A grand metal door blocks your path. Large claw marks are all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. you're pretty sure you're being watched.\n",
                " A minor waterfall in a eerie mountain top marks the entrance to this dungeon. Beyond the waterfall lies a large, deteriorated room. It's covered in puddles of water, dead vermin and rubble. Your torch allows you to see remnants of a small camp, tattered and ravished by time itself. Further ahead are three paths, you take the left. Its twisted trail leads passed countless other pathways and soon you enter a damp area. There are several braziers scattered around, somehow they're still burning, or burning again. What happened in this place? You slowly move onwards, deeper into the dungeon's expanse. You pass countless passages, some are dead ends, others lead to who knows where, or what. You eventually make it to what is likely the final room. A big granite door blocks your path. Strange writing is all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. you're pretty sure you're being watched.\n",
                "A wide granite door in a overcast thicket marks the entrance to this dungeon. Beyond the granite door lies a massive, timeworn room. It's covered in dirt, broken pottery and rubble. Your torch allows you to see remnants of sacks, crates and caskets, demolished and maimed by time itself. Further ahead are three paths, you take the left. Its twisted trail leads downwards and soon you enter a humid area. There's a huge skeleton in the center, along with dozens of human skeletons. What happened in this place? You slowly move onwards, deeper into the dungeon's mysteries. You pass many rooms and passages, some have collapsed, others are dead ends or too dangerous to try. You eventually make it to what is likely the final room. An immense metal door blocks your path. Strange writing is all over it, somehow untouched by time and the elements. You step closer to inspect it and.. wait.. you're pretty sure you're being watched.\n"
                };
            //Random rand = new Random();
            //int index = rand.Next(rooms.Length);
            //string room = rooms[rand.Next(rooms.Length)];
            //return room;//create the value and return it
            //return rooms[rand.Next(rooms.Length)];
            return rooms[new Random().Next(rooms.Length)];//refactored all the way down
        }//end GetRoom()
    }//end class
}//end namespace
