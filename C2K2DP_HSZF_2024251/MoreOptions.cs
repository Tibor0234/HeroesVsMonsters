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
                    //BattleStatistics(ctx);
                    break;
                case 5:
                    SearchBy(ctx);
                    break;
                case 6:
                    ListBy(ctx);
                    break;
                case 7:
                    //Xml(ctx);
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
        public static void SearchBy(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            IEntity[] selected = new IEntity[Math.Max(ctx.Heroes.Count(), ctx.Monsters.Count())];
            bool isHero;
            if (keyInfo.Key == ConsoleKey.H || keyInfo.Key == ConsoleKey.M)
            {
                if (keyInfo.Key == ConsoleKey.H)
                    isHero = true;
                else
                    isHero = false;

                Console.Clear();
                Console.WriteLine("Name:\n");
                if (isHero)
                    Console.WriteLine("Category (C, B, A, S):\n");
                else
                    Console.WriteLine("Level (Vampire, Daemon, Golem, Dragon):\n");
                Console.WriteLine("Strength (1-100):\n");
                Console.WriteLine("Speed (1-100):\n");

                selected = MoreOptionsService.SearchBy(ctx, isHero);
                foreach (var entity in selected)
                {
                    if (entity == null)
                        break;
                    Console.WriteLine(entity);
                }
                Console.ReadKey();
            }                
        }
        public static void ListBy(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            bool isHero;
            if (keyInfo.Key == ConsoleKey.H || keyInfo.Key == ConsoleKey.M)
            {
                if (keyInfo.Key == ConsoleKey.H)
                    isHero = true;
                else
                    isHero = false;
                Console.Clear();
                Console.WriteLine("List by: ");
                int cursor = 0;
                do
                {
                    if (cursor < 0)
                        cursor = 3;
                    else if (cursor > 3)
                        cursor = 0;
                    string[] rows = ["Name", "Category", "Strength", "Speed", "Level"];

                    Console.SetCursorPosition(10, 0);
                    if (isHero)
                        Console.WriteLine(rows[cursor]);
                    else if (cursor == 1)
                        Console.WriteLine(rows[4]);

                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        cursor++;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        cursor--;
                    }

                    Console.WriteLine("\n(Use up / down arrows to navigate, press Enter to select option)");
                    Console.ReadKey();
                } while (keyInfo.Key != ConsoleKey.Enter);
            }
        }
        public static void Xml()
        {

        }
        public static void Reports()
        {

        }
    }
}
