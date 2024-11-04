using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class More_SearchBy
    {
        public static IEntity[] Search(HeroesVsMonstersDbContext ctx, bool isHero)
        {
            string[] properties = new string[4];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            Console.SetCursorPosition(0, 9);
            if (isHero)
            {
                var selected = ctx.Heroes.Where(
                    h => (properties[0].IsNullOrEmpty() || h.Name.Contains(properties[0])) &&
                    (properties[1].IsNullOrEmpty() || h.Category.Equals(properties[1])) &&
                    (properties[2].IsNullOrEmpty() || h.Strength.Equals(int.Parse(properties[2]))) &&
                    (properties[3].IsNullOrEmpty() || h.Speed.Equals(int.Parse(properties[3])))).ToArray();
                return selected;
            }
            else
            {
                var selected = ctx.Monsters.Where(
                    m => (properties[0].IsNullOrEmpty() || m.Name.Contains(properties[0])) &&
                    (properties[1].IsNullOrEmpty() || m.Level.Equals(properties[1])) &&
                    (properties[2].IsNullOrEmpty() || m.Strength.Equals(int.Parse(properties[2]))) &&
                    (properties[3].IsNullOrEmpty() || m.Speed.Equals(int.Parse(properties[3])))).ToArray();
                return selected;
            }
        }
    }
}
