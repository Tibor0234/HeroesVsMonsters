using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IMoreListBy
    {
        public void ChooseEntity();
        public void ListBy(bool isHero);
    }
    public class MoreListBy : IMoreListBy
    {
        IHeroesVsMonstersDbContext ctx;
        IMoreListByService moreListByService;

        public MoreListBy(IHeroesVsMonstersDbContext ctx, IMoreListByService moreListByService)
        {
            this.ctx = ctx;
            this.moreListByService = moreListByService;
        }

        public void ChooseEntity()
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.H)
                ListBy(true);
            else if (keyInfo.Key == ConsoleKey.M)
                ListBy(false);
        }

        public void ListBy(bool isHero)
        {
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
                selection = moreListByService.List("Strength", isHero);
            else
                selection = moreListByService.List("Speed", isHero);

            for (int i = 0; i < selection.Length; i++)
            {
                if (selection[i] == null)
                    break;
                Console.WriteLine($"{i + 1,2}.  |  {selection[i]}");
            }
            Console.ReadKey();
        }
    }
}
