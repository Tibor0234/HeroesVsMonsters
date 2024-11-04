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
    public static class MoreOptions
    {
        public static void ListOptions(HeroesVsMonstersDbContext ctx)
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
            Console.WriteLine("\n(Use up / down arrows to navigate, press Enter to select option)");
            ConsoleKeyInfo keyInfo;
            int cursor = 2;
            do
            {
                if (cursor < 2)
                    cursor = 8;
                else if (cursor > 8)
                    cursor = 2;
                Console.SetCursorPosition(25, cursor);
                Console.Write("<--|");
                keyInfo = Console.ReadKey();
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
            } while (keyInfo.Key != ConsoleKey.Enter);
            switch (cursor)
            {
                case 2:
                    SimulateBattles(ctx);
                    break;
                case 3:
                    AddAbility();
                    break;
                case 4:
                    BattleStatistics(ctx);
                    break;
                case 5:
                    SearchBy(ctx);
                    break;
                case 6:
                    ListBy(ctx);
                    break;
                case 7:
                    Xml(ctx);
                    break;
                case 8:
                    //Reports(ctx);
                    break;
            }
        }
        public static void SimulateBattles(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.Write("Enter how many battles you want to simulate: ");
            int simulationCount = int.Parse(Console.ReadLine());
            Application.More_SimulateBattles.Simulate(ctx, simulationCount);
            Console.WriteLine("\nBattles have been simulated successfully.");
            Console.ReadKey();
        }
        public static void AddAbility()
        {

        }
        public static void BattleStatistics(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.Write("Heroes win rate: ");

            Console.WriteLine(More_BattleStatistics.HeroesWinRate(ctx) + "%\n");
            Console.WriteLine("Monsters Defeated:\n");
            Monster[] defeated = More_BattleStatistics.DefeatedMonsters(ctx);
            for (int i = 0; i < defeated.Length; i++)
            {
                Console.WriteLine($"{i + 1,2}.  |  {defeated[i]}");
            }
            Console.ReadKey();
        }
        public static bool ChooseEntity(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            bool isHero;
            if (keyInfo.Key == ConsoleKey.H)
                return true;
            else
                return false;
        }
        public static void SearchBy(HeroesVsMonstersDbContext ctx)
        {
            bool isHero = ChooseEntity(ctx);
            Console.Clear();
            Console.WriteLine("Name:\n");
            if (isHero) Console.WriteLine("Category (C, B, A, S):\n");
            else Console.WriteLine("Level (Vampire, Daemon, Golem, Dragon):\n");
            Console.WriteLine("Strength (1-100):\n");
            Console.WriteLine("Speed (1-100):\n");

            IEntity[] selected = new IEntity[Math.Max(ctx.Heroes.Count(), ctx.Monsters.Count())];
            selected = Application.More_SearchBy.Search(ctx, isHero);
            for (int i = 0; i < selected.Length; i++)
            {
                if (selected[i] == null)
                    break;
                Console.WriteLine($"{i + 1,2}.  |  {selected[i]}");
            }
            Console.ReadKey();         
        }
        public static void ListBy(HeroesVsMonstersDbContext ctx)
        {
            bool isHero = ChooseEntity(ctx);
            Console.Clear();
            Console.WriteLine("List by: ");
            Console.WriteLine("\n(Use Tab to toggle, press Enter to select option)");
            ConsoleKeyInfo keyInfo;
            bool listStrength = true;
            do
            {
                Console.SetCursorPosition(10, 0);
                if (listStrength)
                    Console.WriteLine("Strength");
                else
                    Console.WriteLine("Speed   ");
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Tab)
                {
                    if (listStrength)
                        listStrength = false;
                    else
                        listStrength = true;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            IEntity[] selection = new IEntity[Math.Max(ctx.Heroes.Count(), ctx.Monsters.Count())];
            Console.SetCursorPosition(0, 4);

            if (listStrength)
                selection = Application.More_ListBy.List(ctx, "Strength", isHero);
            else
                selection = Application.More_ListBy.List(ctx, "Speed", isHero);

            for (int i = 0; i < selection.Length; i++)
            {
                if (selection[i] == null)
                    break;
                Console.WriteLine($"{i+1,2}.  |  {selection[i]}");
            }
            Console.ReadKey();
        }
        public static void Xml(HeroesVsMonstersDbContext ctx)
        {
            More_Xml.ExportEntities(ctx);
        }
        public static void Reports()
        {

        }
    }
}
