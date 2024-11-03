using System.Data.Common;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Application;
using System.Reflection;
using Azure;
using System.Security.Cryptography;

namespace C2K2DP_HSZF_2024251
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SeedDb();
        }
        private static void SeedDb()
        {
            var ctx = new HeroesVsMonstersDbContext();

            //Heroes

            List<Hero> heroes = new List<Hero>() {
            new Hero("Thunder Knight", "A", 75, 85, "LightningStrike"),
            new Hero("Mystic Sage", "S", 90, 70, "ArcaneBlast, ShadowBolt"),
            new Hero("Shadow Assassin", "B", 60, 95, "Stealth"),
            new Hero("Flame Guardian", "A", 80, 75, "Fireball, FlameShield"),
            new Hero("Earth Warden", "C", 50, 55, "HealingTouch"),
            new Hero("Celestial Archer", "S", 85, 90, "StarShot, Stealth"),
            new Hero("Ice Paladin", "B", 70, 65, "FlameShield"),
            new Hero("Void Sorceress", "A", 85, 60, "ShadowBolt, LightningStrike"),
            new Hero("Battle Monk", "C", 55, 50, "PalmStrike"),
            new Hero("Nature Druid", "B", 65, 80, "HealingTouch, VineWhip"),
            };

            foreach (var hero in heroes)
            {
                ctx.Heroes.Add(hero);
            }

            //Monsters

            List<Monster> monsters = new List<Monster> {
            new Monster("Blood Thirster", "Vampire", 40, 70),
            new Monster("Infernal Beast", "Daemon", 85, 60),
            new Monster("Stone Guardian", "Golem", 90, 40),
            new Monster("Flame Wyvern", "Dragon", 100, 80),
            new Monster("Night Stalker", "Vampire", 50, 75),
            new Monster("Shadow Demon", "Daemon", 70, 50),
            new Monster("Iron Golem", "Golem", 95, 30),
            new Monster("Thunder Drake", "Dragon", 90, 85),
            new Monster("Soul Eater", "Vampire", 60, 65),
            new Monster("Chaos Daemon", "Daemon", 80, 55),
            };

            foreach (var monster in monsters)
            {
                ctx.Monsters.Add(monster);
            }

            ctx.SaveChanges();

            MainMenu.Menu(ctx);
        }
    }
}
