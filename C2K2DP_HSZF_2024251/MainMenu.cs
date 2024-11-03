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
    public static class MainMenu
    {
        public static void Menu(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            ListEntities.ListAll(ctx);

            Console.Write("\nEnter battle [Q]");
            Console.SetCursorPosition(35, Console.GetCursorPosition().Top);
            Console.Write("Add new entity [W]");
            Console.SetCursorPosition(70, Console.GetCursorPosition().Top);
            Console.Write("Modify entity [E]");
            Console.SetCursorPosition(105, Console.GetCursorPosition().Top);
            Console.WriteLine("More options [R]");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Q)
            {
                BattleSimulation.PrepareBattle(ctx);
                Menu(ctx);
            }
            if (keyInfo.Key == ConsoleKey.W)
            {
                AppendOrModifyEntity.ChooseEntity(ctx, true);
                Menu(ctx);
            }
            if (keyInfo.Key == ConsoleKey.E)
            {
                AppendOrModifyEntity.ChooseEntity(ctx, false);
                Menu(ctx);
            }
            if (keyInfo.Key == ConsoleKey.R)
            {
                MoreOptions.ListOptions(ctx);
                Menu(ctx);
            }
        }
    }
}
