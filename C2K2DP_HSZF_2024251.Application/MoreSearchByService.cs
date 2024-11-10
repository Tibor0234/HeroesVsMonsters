using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IMoreSearchByService
    {
        public IEntity[] Search(bool isHero);
    }
    public class MoreSearchByService : IMoreSearchByService
    {
        IHeroesVsMonstersDbContext ctx;
        IHeroAbilities heroAbilities;

        public MoreSearchByService(IHeroesVsMonstersDbContext ctx, IHeroAbilities heroAbilities)
        {
            this.ctx = ctx;
            this.heroAbilities = heroAbilities;
        }

        public IEntity[] Search(bool isHero)
        {
            string[] properties = new string[5];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            Console.SetCursorPosition(0, 11);
            Console.WriteLine("List Of Abilities: " + heroAbilities.AllAbilities);
            Console.SetCursorPosition(0, 9);
            properties[4] = Console.ReadLine();
            Console.SetCursorPosition(0, 13);
            string[] abilitiesSplitted = properties[4].Split(", ");
            if (isHero)
            {
                var selected = ctx.Heroes.Where(
                    h => (properties[0].IsNullOrEmpty() || h.Name.ToLower().Contains(properties[0].ToLower())) &&
                    (properties[1].IsNullOrEmpty() || h.Category.ToLower().Equals(properties[1].ToLower())) &&
                    (properties[2].IsNullOrEmpty() || h.Strength.Equals(int.Parse(properties[2]))) &&
                    (properties[3].IsNullOrEmpty() || h.Speed.Equals(int.Parse(properties[3])))&&
                    (properties[4].IsNullOrEmpty() || abilitiesSplitted.All(a => h.Abilities.ToLower().Contains(a.ToLower()))))
                    .ToArray();
                return selected;
            }
            else
            {
                var selected = ctx.Monsters.Where(
                    m => (properties[0].IsNullOrEmpty() || m.Name.Contains(properties[0])) &&
                    (properties[1].IsNullOrEmpty() || m.Level.ToLower().Equals(properties[1].ToLower())) &&
                    (properties[2].IsNullOrEmpty() || m.Strength.Equals(int.Parse(properties[2]))) &&
                    (properties[3].IsNullOrEmpty() || m.Speed.Equals(int.Parse(properties[3])))).ToArray();
                return selected;
            }
        }
    }
}
