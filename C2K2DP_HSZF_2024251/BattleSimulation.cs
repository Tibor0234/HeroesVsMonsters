using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IBattleSimulation
    {
        public void PrepareBattle();
        public void SimulateBattle(int heroId, int monsterId);
    }
    public class BattleSimulation : IBattleSimulation
    {
        IHeroesVsMonstersDbContext ctx;
        IListEntities listEntities;
        IBattleService battleService;

        public BattleSimulation(IHeroesVsMonstersDbContext ctx, IListEntities listEntities, IBattleService battleService)
        {
            this.ctx = ctx;
            this.listEntities = listEntities;
            this.battleService = battleService;
        }

        public void PrepareBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle (Choose Your Hero)\n");
            Console.WriteLine("Heroes:");
            listEntities.ListHeroes();
            Console.Write("\nEnter your hero's number: ");
            int heroId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Clear();
            Console.WriteLine("Battle (Choosing your opponent...)\n");
            Console.WriteLine("Monsters:");
            listEntities.ListMonsters();

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
            battleService.BattleInitialization(hero, monster);

            while (battleService.BattleOn(hero, monster))
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
                        if (battleService.AbilityReady(hero))
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
                                battleService.Attack(hero, monster);
                            //else
                                //heroAbilities.UseAbility(int.Parse(ability));
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
            if (battleService.HeroWon(hero, monster))
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
