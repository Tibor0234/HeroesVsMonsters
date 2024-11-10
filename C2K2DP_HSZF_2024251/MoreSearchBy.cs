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
    public interface IMoreSearchBy
    {
        public void ChooseEntity();
        public void SearchBy(bool isHero);
    }
    public class MoreSearchBy : IMoreSearchBy
    {
        IHeroesVsMonstersDbContext ctx;
        IMoreSearchByService moreSearchByService;

        public MoreSearchBy(IHeroesVsMonstersDbContext ctx, IMoreSearchByService moreSearchByService)
        {
            this.ctx = ctx;
            this.moreSearchByService = moreSearchByService;
        }

        public void ChooseEntity()
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.H)
                SearchBy(true);
            else if (keyInfo.Key == ConsoleKey.M)
                SearchBy(false);
        }
        public void SearchBy(bool isHero)
        {
            Console.Clear();
            Console.WriteLine("Name:\n");
            if (isHero) Console.WriteLine("Category (C, B, A, S):\n");
            else Console.WriteLine("Level (Vampire, Daemon, Golem, Dragon):\n");
            Console.WriteLine("Strength (1-100):\n");
            Console.WriteLine("Speed (1-100):\n");
            Console.WriteLine("Abilities (Ability1, Ability2, ...):\n");

            IEntity[] selected = new IEntity[Math.Max(ctx.Heroes.Count(), ctx.Monsters.Count())];
            selected = moreSearchByService.Search(isHero);
            if (selected.Length == 0) Console.WriteLine("No entity was found with these parameters.");
            for (int i = 0; i < selected.Length; i++)
            {
                if (selected[i] == null)
                    break;
                Console.WriteLine($"{i + 1,2}.  |  {selected[i]}");
            }
            Console.ReadKey();
        }
    }
}
