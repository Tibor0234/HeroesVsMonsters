using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IMoreSimulateBattles
    {
        public void SimulateBattles();
    }
    public class MoreSimulateBattles : IMoreSimulateBattles
    {
        IHeroesVsMonstersDbContext ctx;
        IMoreSimulateBattlesService moreSimulateBattlesService;

        public MoreSimulateBattles(IHeroesVsMonstersDbContext ctx, IMoreSimulateBattlesService moreSimulateBattlesService)
        {
            this.ctx = ctx;
            this.moreSimulateBattlesService = moreSimulateBattlesService;
        }

        public void SimulateBattles()
        {
            Console.Clear();
            Console.Write("Enter how many battles you want to simulate: ");
            int simulationCount = int.Parse(Console.ReadLine());
            moreSimulateBattlesService.Simulate(simulationCount);
            Console.WriteLine("\nBattles have been simulated successfully.");
            Console.ReadKey();
        }
    }
}
