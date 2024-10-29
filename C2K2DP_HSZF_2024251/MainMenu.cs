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
        AppendEntity appendEntity;
        ModifyEntity modifyEntity;
        public MainMenu(HeroesVsMonstersDbContext ctx, BattleSimulation battleSimulation, AppendEntity appendEntity, ModifyEntity modifyEntity)
        {
            this.ctx = ctx;
            this.battleSimulation = battleSimulation;
            this.appendEntity = appendEntity;
            this.modifyEntity = modifyEntity;
        }
        public void Menu()
        {
            Console.Clear();
            ListEntities();

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
                appendEntity.Append();
                Menu();
            }
            if (keyInfo.Key == ConsoleKey.E)
            {
                modifyEntity.Modify();
                Menu();
            }
        }
        public void ListEntities()
        {
            Console.Write("Heroes:");
            Console.SetCursorPosition(80, Console.GetCursorPosition().Top);
            Console.WriteLine("Monsters:");
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
            Console.WriteLine();
            
            int i = 0;
            while (i < ctx.Heroes.Count() && i < ctx.Monsters.Count())
            {
                Console.Write(ctx.Heroes.ElementAt(i));
                Console.SetCursorPosition(80, Console.GetCursorPosition().Top);
                Console.Write(ctx.Monsters.ElementAt(i));
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                Console.WriteLine();
                i++;
            }
            while (i < ctx.Heroes.Count())
            {
                Console.WriteLine(ctx.Heroes.ElementAt(i));
                i++;
            }
            while (i < ctx.Monsters.Count())
            {
                Console.SetCursorPosition(80, Console.GetCursorPosition().Top);
                Console.WriteLine(ctx.Heroes.ElementAt(i));
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                i++;
            }
            Console.WriteLine("\nBattles:\n");
            for (int j = 0; j < ctx.Battles.Count(); j++)
            {
                Console.WriteLine(ctx.Battles.ElementAt(j));
            }
        }
    }
}
