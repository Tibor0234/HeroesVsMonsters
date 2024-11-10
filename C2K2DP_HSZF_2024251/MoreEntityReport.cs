using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IMoreEntityReport
    {
        public void ChooseTheme();
    }
    public class MoreEntityReport : IMoreEntityReport
    {
        IMoreEntityReportService moreEntityReportService;
        public MoreEntityReport(IMoreEntityReportService moreEntityReportService)
        {
            this.moreEntityReportService = moreEntityReportService;
        }
        public void ChooseTheme()
        {
            Console.Clear();
            Console.WriteLine("Choose theme:");
            ConsoleKeyInfo keyInfo;
            int cursor = 2;
            string[] lines = ["", "", "Heroes.txt (Average strength and speed by category)", "Monsters.txt (Undefeated monsters)", "Monsters.xml (Defeated monsters name and level in a month)", "Battles.txt (Battle count by heroes and win rates last year)"];
            Console.SetCursorPosition(0, 3);
            for (int i = 3; i < lines.Length; i++)
            {
                Console.WriteLine($" {lines[i]} ");
            }
            do
            {
                if (cursor < 2)
                    cursor = 5;
                else if (cursor > 5)
                    cursor = 2;

                Console.SetCursorPosition(0, cursor);
                Console.WriteLine($"<{lines[cursor]}>");

                Console.SetCursorPosition(0, 6);
                keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, cursor);
                    Console.WriteLine($" {lines[cursor]} ");
                    cursor++;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, cursor);
                    Console.WriteLine($" {lines[cursor]} ");
                    cursor--;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            GetReport(cursor);
        }
        private void GetReport(int cursor)
        {
            switch (cursor)
            {
                case 2:
                    moreEntityReportService.HeroReport();
                    break;
                case 3:
                    moreEntityReportService.MonsterReport();
                    break;
                case 4:
                    moreEntityReportService.MonsterReportXml();
                    break;
                case 5:
                    moreEntityReportService.BattleReport();
                    break;
            }
            Console.Clear();
            Console.WriteLine("The report was successfully created.");
            Console.ReadKey();
        }
    }
}
