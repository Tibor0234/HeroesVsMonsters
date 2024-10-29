using Azure.Identity;
using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public class MainMenu
    {
        HeroesVsMonstersDbContext ctx;
        BattleSimulation battleSimulation;
        AppendOrModifyEntity appendOrModifyEntity;
        public MainMenu(HeroesVsMonstersDbContext ctx, BattleSimulation battleSimulation, AppendOrModifyEntity appendOrModifyEntity)
        {
            this.ctx = ctx;
            this.battleSimulation = battleSimulation;
            this.appendOrModifyEntity = appendOrModifyEntity;
        }
        public void Menu()
        {
            Console.Clear();
            ListEntities.ListAll(ctx);

            Console.Write("\nSimulate battle: [Q]");
            Console.SetCursorPosition(40, Console.GetCursorPosition().Top);
            Console.Write("Add new entity: [W]");
            Console.SetCursorPosition(80, Console.GetCursorPosition().Top);
            Console.WriteLine("Modify entity: [E]");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Q)
            {
                battleSimulation.PrepareBattle();
                Menu();
            }
            if (keyInfo.Key == ConsoleKey.W)
            {
                appendOrModifyEntity.ChooseEntity(true);
                Menu();
            }
            if (keyInfo.Key == ConsoleKey.E)
            {
                appendOrModifyEntity.ChooseEntity(false);
                Menu();
            }
        }
    }
}
