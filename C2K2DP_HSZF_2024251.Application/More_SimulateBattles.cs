using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class More_SimulateBattles
    {
        public static void Simulate(HeroesVsMonstersDbContext ctx, int simuationCount)
        {
            Random rnd = new Random();
            for (int i = 0; i < simuationCount; i++)
            {
                int heroId = rnd.Next(1, ctx.Heroes.Count() + 1);
                int monsterId = rnd.Next(1, ctx.Monsters.Count() + 1);
                Hero hero = ctx.Heroes.Find(heroId);
                Monster monster = ctx.Monsters.Find(monsterId);
                DateTime dateTime = new DateTime(rnd.Next(2020, 2031), rnd.Next(1, 13), rnd.Next(1,29));
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
    }
}
