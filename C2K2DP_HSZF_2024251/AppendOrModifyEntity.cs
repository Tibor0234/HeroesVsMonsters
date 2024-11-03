using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public static class AppendOrModifyEntity
    {
        public static void ChooseEntity(HeroesVsMonstersDbContext ctx, bool append)
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.H)
            {
                if (append)
                    AppendHero(ctx);
                else
                    ModifyHero(ctx);
            }
            else if (keyInfo.Key == ConsoleKey.M)
            {
                if (append)
                    AppendMonster(ctx);
                else
                    ModifyMonster(ctx);
            }
        }
        public static void AppendHero(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Name:\n");
            Console.WriteLine("Category (C, B, A, S):\n");
            Console.WriteLine("Strength (1-100):\n");
            Console.WriteLine("Speed (1-100):\n");
            Console.WriteLine("Abilities (Ability1, Ability2, ...):");

            string[] properties = AppendOrModifyEntityService.GetHeroProperties(true);

            Console.SetCursorPosition(0, 13);
            if (properties[0] != "failed")
            {
                Console.WriteLine("Hero added successfully.");
                Hero newHero = new Hero(properties[0], properties[1], int.Parse(properties[2]), int.Parse(properties[3]), properties[4]);
                ctx.Heroes.Add(newHero);
                ctx.SaveChanges();
            }
            else
            {
                Console.WriteLine("Invalid input, hero cannot be added.");
            }
            Console.ReadKey();
        }

        public static void AppendMonster(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Name:\n");
            Console.WriteLine("Level (Vampire, Daemon, Golem, Dragon):\n");
            Console.WriteLine("Strength (1-100):\n");
            Console.WriteLine("Speed (1-100):\n");

            string[] properties = AppendOrModifyEntityService.GetMonsterProperties(true);
            
            Console.SetCursorPosition(0, 9);
            if (properties[0] != "failed")
            {
                Console.WriteLine("Monster added successfully.");
                Monster newMonster = new Monster(properties[0], properties[1], int.Parse(properties[2]), int.Parse(properties[3]));
                ctx.Monsters.Add(newMonster);
                ctx.SaveChanges();
            }
            else
            {
                Console.WriteLine("Invalid input, monster cannot be added.");
            }
            Console.ReadKey();
        }

        public static void ModifyHero(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Heroes:\n");
            ListEntities.ListHeroes(ctx);
            Console.Write("\nEnter your hero's number: ");

            int heroId = int.Parse(Console.ReadLine());
            Hero hero = ctx.Heroes.Find(heroId);

            Console.Clear();
            Console.WriteLine($"Name: [{hero.Name}]\n");
            Console.WriteLine($"Category (C, B, A, S): [{hero.Category}]\n");
            Console.WriteLine($"Strength (1-100): [{hero.Strength}]\n");
            Console.WriteLine($"Speed (1-100): [{hero.Speed}]\n");
            Console.WriteLine($"Abilities (Ability1, Ability2, ...): [{hero.Abilities}]");
            Console.SetCursorPosition(0, 13);
            Console.WriteLine("(Leave blank to keep previus setting)");

            string[] properties = AppendOrModifyEntityService.GetHeroProperties(false);

            Console.SetCursorPosition(0, 15);
            if (properties[0] != "failed")
            {
                Console.WriteLine("Hero modified successfully.");
                AppendOrModifyEntityService.SetHeroProperties(hero, properties);
                ctx.Heroes.Update(hero);
            }
            else
            {
                Console.WriteLine("Invalid input, hero cannot be modified.");
            }
            Console.ReadKey();
        }

        public static void ModifyMonster(HeroesVsMonstersDbContext ctx)
        {
            Console.Clear();
            Console.WriteLine("Monsters:\n");
            ListEntities.ListHeroes(ctx);
            Console.Write("\nEnter your monster's number: ");

            int monsterId = int.Parse(Console.ReadLine());
            Monster monster = ctx.Monsters.Find(monsterId);

            Console.Clear();
            Console.WriteLine($"Name: [{monster.Name}]\n");
            Console.WriteLine($"Level (Vampire, Daemon, Golem, Dragon): [{monster.Level}]\n");
            Console.WriteLine($"Strength (1-100): [{monster.Strength}]\n");
            Console.WriteLine($"Speed (1-100): [{monster.Speed}]\n");
            Console.SetCursorPosition(0, 9);
            Console.WriteLine("(Leave blank to keep previus setting)");

            string[] properties = AppendOrModifyEntityService.GetMonsterProperties(false);
            
            Console.SetCursorPosition(0, 11);
            if (properties[0] != "failed")
            {
                Console.WriteLine("Monster modified successfully.");
                AppendOrModifyEntityService.SetMonsterProperties(monster, properties);
                ctx.Monsters.Update(monster);
            }
            else
            {
                Console.WriteLine("Invalid input, monster cannot be modified.");
            }
            Console.ReadKey();
        }
    }
}
