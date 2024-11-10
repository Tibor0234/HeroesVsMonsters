using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IMoreOptions
    {
        public void ListOptions();
        public void SelectOption(int cursor);
    }
    public class MoreOptions : IMoreOptions
    {
        IHeroesVsMonstersDbContext ctx;
        IMoreBattleStatistics moreBattleStatistics;
        IMoreSearchBy moreSearchBy;
        IMoreListBy moreListBy;
        IMoreSimulateBattles moreSimulateBattles;
        IMoreXml moreXml;
        IMoreEntityReport moreEntityReport;

        public MoreOptions(IHeroesVsMonstersDbContext ctx, IMoreBattleStatistics moreBattleStatistics, IMoreSearchBy moreSearchBy, IMoreListBy moreListBy, IMoreSimulateBattles moreSimulateBattles, IMoreXml moreXml, IMoreEntityReport moreEntityReport)
        {
            this.ctx = ctx;
            this.moreBattleStatistics = moreBattleStatistics;
            this.moreSearchBy = moreSearchBy;
            this.moreListBy = moreListBy;
            this.moreSimulateBattles = moreSimulateBattles;
            this.moreXml = moreXml;
            this.moreEntityReport = moreEntityReport;
        }

        public void ListOptions()
        {
            int cursor = 2;
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                Console.WriteLine("More Options:\n");
                Console.WriteLine("- Simulate battles");
                Console.WriteLine("- Add ability");
                Console.WriteLine("- Battle statistics");
                Console.WriteLine("- Search by");
                Console.WriteLine("- List by");
                Console.WriteLine("- Xml export and import");
                Console.WriteLine("- Reports");
                Console.WriteLine("\n(Use up / down arrows to navigate, press Enter to select option or press Esc to return to menu)");
                do
                {
                    if (cursor < 2)
                        cursor = 8;
                    else if (cursor > 8)
                        cursor = 2;
                    Console.SetCursorPosition(25, cursor);
                    Console.Write("<--|");
                    keyInfo = Console.ReadKey(intercept: true);
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        Console.SetCursorPosition(25, cursor);
                        Console.Write("    ");
                        cursor++;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        Console.SetCursorPosition(25, cursor);
                        Console.Write("    ");
                        cursor--;
                    }
                } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

                if (keyInfo.Key == ConsoleKey.Enter) SelectOption(cursor);

            } while (keyInfo.Key != ConsoleKey.Escape);
        }
        public void SelectOption(int cursor)
        {
            switch (cursor)
            {
                case 2:
                    moreSimulateBattles.SimulateBattles();
                    break;
                case 3:
                    //AddAbility();
                    break;
                case 4:
                    moreBattleStatistics.BattleStatistics();
                    break;
                case 5:
                    moreSearchBy.ChooseEntity();
                    break;
                case 6:
                    moreListBy.ChooseEntity();
                    break;
                case 7:
                    moreXml.ChooseMethod();
                    break;
                case 8:
                    moreEntityReport.ChooseTheme();
                    break;
            }
        }
    }
}
