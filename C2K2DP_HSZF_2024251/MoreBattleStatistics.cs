using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IMoreBattleStatistics
    {
        public void BattleStatistics();
    }
    public class MoreBattleStatistics : IMoreBattleStatistics
    {
        IHeroesVsMonstersDbContext ctx;
        IMoreBattleStatisticsSevice moreBattleStatisticsSevice;

        public MoreBattleStatistics(IHeroesVsMonstersDbContext ctx, IMoreBattleStatisticsSevice moreBattleStatisticsSevice)
        {
            this.ctx = ctx;
            this.moreBattleStatisticsSevice = moreBattleStatisticsSevice;
        }

        public void BattleStatistics()
        {
            Console.Clear();
            Console.Write("Heroes win rate: ");

            Console.WriteLine(moreBattleStatisticsSevice.HeroesWinRate() + "%\n");
            Console.WriteLine("Monsters Defeated:\n");
            Monster[] defeated = moreBattleStatisticsSevice.DefeatedMonsters();
            for (int i = 0; i < defeated.Length; i++)
            {
                Console.WriteLine($"{i + 1,2}.  |  {defeated[i]}");
            }
            Console.ReadKey();
        }
    }
}
