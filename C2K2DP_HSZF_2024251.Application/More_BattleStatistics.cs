using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class More_BattleStatistics
    {
        public static float HeroesWinRate(HeroesVsMonstersDbContext ctx)
        {
            if (ctx.Battles.Count() == 0)
                return 0;
            float heroesWon = ctx.Battles.Count(b => b.Result == "Hero won");
            float totalBattles = ctx.Battles.Count();
            float rate = (heroesWon / totalBattles) * 100;
            return float.Round(rate,2);
        }

        public static Monster[] DefeatedMonsters(HeroesVsMonstersDbContext ctx)
        {
            var defeatedMonsters = ctx.Monsters.Where(m => m.Battles.Where(b => b.Result == "Hero won").Count() > 0);
            return defeatedMonsters.ToArray();
        }
    }
}
