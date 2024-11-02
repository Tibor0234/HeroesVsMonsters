using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (result == 0)
                {
                    Battle newBattle = new Battle(hero, monster, DateTime.Now, "Hero won");
                    ctx.Battles.Add(newBattle);
                    ctx.SaveChanges();
                }
                else
                {
                    Battle newBattle = new Battle(hero, monster, DateTime.Now, "Monster won");
                    ctx.Battles.Add(newBattle);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
