using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class MoreOptionsService
    {
        public static void BattleSimulation(HeroesVsMonstersDbContext ctx, int simuationCount)
        {
            Random rnd = new Random();
            for (int i = 0; i < simuationCount; i++)
            {
                int heroId = rnd.Next(1, ctx.Heroes.Count() + 1);
                int monsterId = rnd.Next(1, ctx.Monsters.Count() + 1);
                Hero hero = ctx.Heroes.Find(heroId);
                Monster monster = ctx.Monsters.Find(monsterId);
                int result = rnd.Next(0, 2);
                Battle newBattle;
                if (result == 0)
                    newBattle = new Battle(hero, monster, DateTime.Now, "Hero won");
                else
                    newBattle = new Battle(hero, monster, DateTime.Now, "Monster won");
                hero.Battles.Add(newBattle);
                monster.Battles.Add(newBattle);
                ctx.Battles.Add(newBattle);
                ctx.SaveChanges();
            }
        }

        public static IEntity[] SearchBy(HeroesVsMonstersDbContext ctx, bool isHero)
        {
            string[] properties = new string[4];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            Console.SetCursorPosition(0, 9);
            if (isHero)
            {
                var selected = ctx.Heroes.Where(
                    h => (properties[0].IsNullOrEmpty() || h.Name.Contains(properties[0])) &&
                    (properties[1].IsNullOrEmpty() || h.Category.Equals(properties[1])) &&
                    (properties[2].IsNullOrEmpty() || h.Strength.Equals(int.Parse(properties[2]))) &&
                    (properties[3].IsNullOrEmpty() || h.Speed.Equals(int.Parse(properties[3])))).ToArray();
                return selected;
            }
            else
            {
                var selected = ctx.Monsters.Where(
                    h => (properties[0].IsNullOrEmpty() || h.Name.Contains(properties[0])) &&
                    (properties[1].IsNullOrEmpty() || h.Level.Equals(properties[1])) &&
                    (properties[2].IsNullOrEmpty() || h.Strength.Equals(int.Parse(properties[2]))) &&
                    (properties[3].IsNullOrEmpty() || h.Speed.Equals(int.Parse(properties[3])))).ToArray();
                return selected;
            }
        }
    }
}
