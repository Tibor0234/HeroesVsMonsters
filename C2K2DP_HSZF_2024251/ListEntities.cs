using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public static class ListEntities
    {
        public static void ListAll(HeroesVsMonstersDbContext ctx)
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
                Console.WriteLine(ctx.Monsters.ElementAt(i));
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                i++;
            }
            Console.WriteLine("\nBattles:\n");
            for (int j = 0; j < ctx.Battles.Count(); j++)
            {
                Console.WriteLine(ctx.Battles.ElementAt(j));
            }
        }

        public static void ListHeroes(HeroesVsMonstersDbContext ctx)
        {
            int i = 0;
            while (i < ctx.Heroes.Count())
            {
                Console.WriteLine(ctx.Heroes.ElementAt(i));
                i++;
            }
        }
        public static void ListMonsters(HeroesVsMonstersDbContext ctx)
        {
            int i = 0;
            while (i < ctx.Monsters.Count())
            {
                Console.WriteLine(ctx.Monsters.ElementAt(i));
                i++;
            }
        }
    }
}
