using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public static class BattleSimulation
    {
        public static void PrepareBattle(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Battle (Choose Your Hero)\n");
            Console.WriteLine("Heroes:");
            ListEntities.ListHeroes(ctx);
            Console.Write("\nEnter your hero's number: ");
            int heroId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Clear();
            Console.WriteLine("Battle (Choosing your opponent...)\n");
            Console.WriteLine("Monsters:");
            ListEntities.ListMonsters(ctx);

            int draw = BattleService.DrawOpponent();

            for (int i = 0; i < draw; i++)
            {
                if (i % ctx.Monsters.Count() + 3 == 3)
                {
                    Console.SetCursorPosition(50, ctx.Monsters.Count() + 2);
                    Console.Write("    ");
                }
                else
                {
                    Console.SetCursorPosition(50, i % ctx.Monsters.Count() + 2);
                    Console.Write("    ");
                }
                Console.SetCursorPosition(50, i % ctx.Monsters.Count() + 3);
                Console.Write("<--|");
                Thread.Sleep(i);

                if (i == draw - 1)
                {
                    Console.SetCursorPosition(0, ctx.Monsters.Count() + 3);
                    Console.ReadKey();
                }
            }
            int monsterId = BattleService.GetOpponentId(ctx, draw);
            
            SimulateBattle(heroId, monsterId, ctx);
        }
        public static void SimulateBattle(int heroId, int monsterId, HeroesVsMonstersDbContext ctx)
        {
            Hero hero = BattleService.FindHero(ctx, heroId);
            Monster monster = BattleService.FindMonster(ctx, monsterId);
            BattleService.BattleInitialization(hero, monster);
            BattleService.round = false;

            while (BattleService.BattleOn(hero, monster))
            {
                Console.Clear();
                Console.Write(hero.ToStringInBattle());
                Console.SetCursorPosition(70, Console.GetCursorPosition().Top);
                Console.WriteLine(monster.ToStringInBattle());
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                if (!BattleService.BattleEnded)
                {
                    if (BattleService.Round)
                    {
                        if (hero.Manna >= hero.MaxManna)
                        {
                            string[] listOfAbilities = hero.Abilities.Split(" ");
                            Console.Write("\nAbilities: ");
                            for (int i = 0; i < listOfAbilities.Length; i++)
                            {
                                Console.Write($"{i + 1}. {listOfAbilities[i]}");
                            }
                            Console.Write("\nType ability number to use ability or press [Enter] for regular attack: ");
                            string ability = Console.ReadLine();
                            if (ability.IsNullOrEmpty())
                                BattleService.Attack(hero, monster);
                            //else
                                //heroAbilities.UseAbility(int.Parse(ability));
                        }
                        else
                        {
                            Console.WriteLine($"\nAbilities: {hero.Manna}%");
                            BattleService.Attack(hero, monster);
                            Console.ReadKey();
                        }
                    }                    
                    else
                    {
                        Console.WriteLine("\nMonster attacking...");
                        BattleService.Attack(monster, hero);
                        Console.ReadKey();
                    }
                }
            }
            if (BattleService.HeroWon(ctx, hero, monster))
            {
                Console.WriteLine("\nVictory");
            }
            else
            {
                Console.WriteLine("\nDefeat");
            }
            Console.ReadKey();
        }
    }
}
