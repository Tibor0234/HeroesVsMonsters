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
    public class BattleSimulation
    {
        HeroesVsMonstersDbContext ctx;
        BattleService battleService;
        public BattleSimulation(HeroesVsMonstersDbContext ctx, BattleService battleService)
        {
            this.ctx = ctx;
            this.battleService = battleService;
        }
        public void ListHeroes()
        {
            Console.Write("Heroes:\n");
            int i = 0;
            while (i < ctx.Heroes.Count())
            {
                Console.WriteLine(ctx.Heroes.ElementAt(i));
                i++;
            }
        }
        public void ListMonsters()
        {
            Console.Write("Monsters:\n");
            int i = 0;
            while (i < ctx.Monsters.Count())
            {
                Console.WriteLine(ctx.Monsters.ElementAt(i));
                i++;
            }
        }
        public void PrepareBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle (Choose Your Hero)\n");
            ListHeroes();
            Console.Write("\nEnter your hero's number: ");
            int heroId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Clear();
            Console.WriteLine("Battle (Choosing your opponent...)\n");
            ListMonsters();

            int draw = battleService.DrawOpponent();

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
            int monsterId = battleService.GetOpponentId(draw);
            
            SimulateBattle(heroId, monsterId);
        }
        public void SimulateBattle(int heroId, int monsterId)
        {
            Hero hero = battleService.FindHero(heroId);
            Monster monster = battleService.FindMonster(monsterId);
            HeroAbilities heroAbilities = new(hero);
            battleService.BattleInitialization();

            while (battleService.BattleOn())
            {
                Console.Clear();
                Console.Write(hero.ToStringInBattle());
                Console.SetCursorPosition(70, Console.GetCursorPosition().Top);
                Console.WriteLine(monster.ToStringInBattle());
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                if (!battleService.BattleEnded)
                {
                    if (battleService.Round)
                    {
                        if (hero.Manna >= hero.MaxManna)
                        {
                            Console.Write("\nAbilities: " + hero.Abilities + "\nType ability name to use ability or press [Enter] for regular attack: ");
                            string ability = Console.ReadLine();
                            if (ability.IsNullOrEmpty())
                                battleService.Attack(hero, monster);
                            else
                                heroAbilities.UseAbility(ability);
                        }
                        else
                        {
                            Console.WriteLine($"\nAbilities: {hero.Manna}%");
                            battleService.Attack(hero, monster);
                            Console.ReadKey();
                        }
                    }                    
                    else
                    {
                        Console.WriteLine("\nMonster attacking...");
                        battleService.Attack(monster, hero);
                        Console.ReadKey();
                    }
                }
            }
            if (battleService.HeroWon())
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
