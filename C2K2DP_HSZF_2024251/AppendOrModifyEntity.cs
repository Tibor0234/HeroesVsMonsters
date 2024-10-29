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
    public class AppendOrModifyEntity
    {
        HeroesVsMonstersDbContext ctx;
        public AppendOrModifyEntity(HeroesVsMonstersDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void ChooseEntity(bool append)
        {
            Console.Clear();
            Console.WriteLine("Choose entity:\n\nHero [H]\t\t\tMonster [M]");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.H)
            {
                if (append)
                    AppendHero();
                else
                    ModifyHero();
            }
            else if (keyInfo.Key == ConsoleKey.M)
            {
                if (append)
                    AppendMonster();
                else
                    ModifyMonster();
            }
        }
        public string[] SetHeroProperties(bool append)
        {
            string[] properties = new string[5];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            if (!Validation.ValidateName(properties[0], append))
                return ["failed"];
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            if (!Validation.ValidateCategory(properties[1], append))
                return ["failed"];
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            if (!Validation.ValidateStrengthAndSpeed(properties[2], append))
                return ["failed"];
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            if (!Validation.ValidateStrengthAndSpeed(properties[3], append))
                return ["failed"];

            string listOfAbilities = string.Join(", ", typeof(HeroAbilities)
            .GetMethods()
            .Where(m => m.Name.Contains("Ability_"))
            .Select(m => m.Name.Substring(8)));
            Console.SetCursorPosition(0, 11);
            Console.Write("List Of Abilities: " + listOfAbilities);

            Console.SetCursorPosition(0, 9);
            properties[4] = Console.ReadLine();
            if (!Validation.ValidateAbilities(properties[4], append))
                return ["failed"];

            return properties;
        }
        public string[] SetMonsterProperties(bool append)
        {
            string[] properties = new string[4];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            if (!Validation.ValidateName(properties[0], append))
                return ["failed"];
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            if (!Validation.ValidateLevel(properties[1], append))
                return ["failed"];
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            if (!Validation.ValidateStrengthAndSpeed(properties[2], append))
                return ["failed"];
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            if (!Validation.ValidateStrengthAndSpeed(properties[3], append))
                return ["failed"];

            return properties;
        }
        public void AppendHero()
        {
            Console.Clear();
            Console.WriteLine("Name:\n");
            Console.WriteLine("Category (C, B, A, S):\n");
            Console.WriteLine("Strength (1-100):\n");
            Console.WriteLine("Speed (1-100):\n");
            Console.WriteLine("Abilities (Ability1, Ability2, ...):");

            string[] properties = SetHeroProperties(true);

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

        public void AppendMonster()
        {
            Console.Clear();
            Console.WriteLine("Name:\n");
            Console.WriteLine("Level (Vampire, Daemon, Golem, Dragon):\n");
            Console.WriteLine("Strength (1-100):\n");
            Console.WriteLine("Speed (1-100):\n");

            string[] properties = SetMonsterProperties(true);
            
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

        public void ModifyHero()
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

            string[] properties = SetHeroProperties(false);

            Console.SetCursorPosition(0, 15);
            if (properties[0] != "failed")
            {
                Console.WriteLine("Hero modified successfully.");
                if (!properties[0].IsNullOrEmpty())
                    hero.Name = properties[0];
                if (!properties[1].IsNullOrEmpty())
                    hero.Category = properties[1];
                if (!properties[2].IsNullOrEmpty())
                    hero.Strength = int.Parse(properties[2]);
                if (!properties[3].IsNullOrEmpty())
                    hero.Speed = int.Parse(properties[3]);
                if (!properties[4].IsNullOrEmpty())
                    hero.Abilities = properties[4];
                ctx.Heroes.Update(hero);
            }
            else
            {
                Console.WriteLine("Invalid input, hero cannot be added.");
            }
            Console.ReadKey();
        }

        public void ModifyMonster()
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

            string[] properties = SetMonsterProperties(false);
            
            Console.SetCursorPosition(0, 11);
            if (properties[0] != "failed")
            {
                Console.WriteLine("Monster modified successfully.");
                if (!properties[0].IsNullOrEmpty())
                    monster.Name = properties[0];
                if (!properties[1].IsNullOrEmpty())
                    monster.Level = properties[1];
                if (!properties[2].IsNullOrEmpty())
                    monster.Strength = int.Parse(properties[2]);
                if (!properties[3].IsNullOrEmpty())
                    monster.Speed = int.Parse(properties[3]);
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
