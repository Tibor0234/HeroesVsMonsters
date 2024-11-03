using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
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
                    BattleStatistics();
                    break;
                case 5:
                    SearchBy();
                    break;
                case 6:
                    ListBy();
                    break;
                case 7:
                    Xml();
                    break;
                case 8:
                    Reports();
                    break;
            }
        }
        public static void SimulateBattles(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.Write("Enter how many battles you want to simulate: ");
            int simulationCount = int.Parse(Console.ReadLine());
            MoreOptionsService.BattleSimulation(ctx, simulationCount);
            Console.WriteLine("\nBattles have been simulated successfully.");
            Console.ReadKey();
        }
        public static void AddAbility()
        {

        }
        public static void BattleStatistics()
        {

        }
        public static void SearchBy()
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.H)
            {

            }
            else if (keyInfo.Key == ConsoleKey.M)
            {

            }
        }
        public static void ListBy()
        {

        }
        public static void Xml()
        {

        }
        public static void Reports()
        {

        }
    }
}
