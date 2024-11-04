using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class More_ListBy
    {
        public static IEntity[] List(HeroesVsMonstersDbContext ctx, string propertyName, bool isHero)
        {
            if (isHero)
            {
                return ctx.Heroes.OrderByDescending(h => EF.Property<Hero>(h, propertyName)).ToArray();
            }
            else
            {
                return ctx.Monsters.OrderByDescending(h => EF.Property<Monster>(h, propertyName)).ToArray();
            }
        }
    }
}
