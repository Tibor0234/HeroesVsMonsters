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
        IHeroesVsMonstersDbContext ctx;
        IListEntities listEntities;
        IBattleSimulation battleSimulation;
        IAppendOrModifyEntity appendOrModifyEntity;
        IMoreOptions moreOptions;

        public MainMenu(IHeroesVsMonstersDbContext ctx, IListEntities listEntities, IBattleSimulation battleSimulation, IAppendOrModifyEntity appendOrModifyEntity, IMoreOptions moreOptions)
            
        {
            this.ctx = ctx;
            this.listEntities = listEntities;
            this.battleSimulation = battleSimulation;
            this.appendOrModifyEntity = appendOrModifyEntity;
            this.moreOptions = moreOptions;
        }

        public void Menu()
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                listEntities.ListAll();

                Console.Write("\nEnter battle [Q]");
                Console.SetCursorPosition(25, Console.GetCursorPosition().Top);
                Console.Write("Add new entity [W]");
                Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
                Console.Write("Modify entity [E]");
                Console.SetCursorPosition(75, Console.GetCursorPosition().Top);
                Console.Write("More options [R]");
                Console.SetCursorPosition(100, Console.GetCursorPosition().Top);
                Console.WriteLine("End program [Esc]");

                keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    battleSimulation.PrepareBattle();
                }
                if (keyInfo.Key == ConsoleKey.W)
                {
                    appendOrModifyEntity.ChooseEntity(true);
                }
                if (keyInfo.Key == ConsoleKey.E)
                {
                    appendOrModifyEntity.ChooseEntity(false);
                }
                if (keyInfo.Key == ConsoleKey.R)
                {
                    moreOptions.ListOptions();
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
        }
    }
}
