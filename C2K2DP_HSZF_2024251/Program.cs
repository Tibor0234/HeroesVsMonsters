using System.Data.Common;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Application;
using System.Reflection;
using Azure;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace C2K2DP_HSZF_2024251
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=HeroesVsMonsters;Integrated Security=True;MultipleActiveResultSets=true";

            var serviceCollection = new ServiceCollection();

            IServiceProvider serviceProvider = ConfigContainer(serviceCollection, connectionString);

            HeroesVsMonstersDbContext ctx = serviceProvider.GetService<HeroesVsMonstersDbContext>();

            SeedDb(ctx);

            var mainMenu = serviceProvider.GetService<MainMenu>();

            mainMenu.Menu();
        }
        private static IServiceProvider ConfigContainer(ServiceCollection serviceProvider, string connectionString)
        {
            return
            serviceProvider
            .AddDbContext<IHeroesVsMonstersDbContext, HeroesVsMonstersDbContext>(options => options.UseSqlServer(connectionString))
            .AddSingleton<IMoreXmlService, MoreXmlService>()
            .AddSingleton<IMoreSimulateBattlesService, MoreSimulateBattlesService>()
            .AddSingleton<IMoreListByService, MoreListByService>()
            .AddSingleton<IMoreSearchByService, MoreSearchByService>()
            .AddSingleton<IMoreBattleStatisticsSevice, MoreBattleStatisticsSevice>()
            .AddSingleton<IMoreXml, MoreXml>()
            .AddSingleton<IMoreSimulateBattles, MoreSimulateBattles>()
            .AddSingleton<IMoreListBy, MoreListBy>()
            .AddSingleton<IMoreSearchBy, MoreSearchBy>()
            .AddSingleton<IMoreBattleStatistics, MoreBattleStatistics>()
            .AddSingleton<IHeroAbilities, HeroAbilities>()
            .AddSingleton<IValidation, Validation>()
            .AddSingleton<IAppendOrModifyEntityService, AppendOrModifyEntityService>()
            .AddSingleton<IListEntities, ListEntities>()
            .AddSingleton<IBattleService, BattleService>()
            .AddSingleton<IMoreOptions, MoreOptions>()
            .AddSingleton<IAppendOrModifyEntity, AppendOrModifyEntity>()
            .AddSingleton<IBattleSimulation, BattleSimulation>()
            .AddSingleton<IMoreEntityReport, MoreEntityReport>()
            .AddSingleton<IMoreEntityReportService, MoreEntityReportService>()
            .AddSingleton<MainMenu>()
            .BuildServiceProvider();
        }
        private static void SeedDb(HeroesVsMonstersDbContext ctx)
        {
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

            ctx.Heroes.AddRange(heroes);

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

            ctx.Monsters.AddRange(monsters);

            ctx.SaveChanges();

            List<Battle> battles = new List<Battle>() {
            new Battle(heroes[0], monsters[3], new DateTime(2024, 10, 21), "Hero won"),
            new Battle(heroes[2], monsters[1], new DateTime(2024, 8, 14), "Monster won"),
            new Battle(heroes[5], monsters[8], new DateTime(2024, 9, 7), "Hero won"),
            new Battle(heroes[4], monsters[6], new DateTime(2024, 11, 3), "Monster won"),
            new Battle(heroes[1], monsters[2], new DateTime(2024, 10, 15), "Hero won"),
            new Battle(heroes[6], monsters[0], new DateTime(2024, 12, 5), "Hero won"),
            new Battle(heroes[9], monsters[5], new DateTime(2024, 8, 21), "Monster won"),
            new Battle(heroes[7], monsters[7], new DateTime(2024, 7, 19), "Monster won"),
            new Battle(heroes[3], monsters[4], new DateTime(2024, 6, 9), "Hero won"),
            new Battle(heroes[8], monsters[9], new DateTime(2024, 9, 30), "Monster won")
            };

            ctx.Battles.AddRange(battles);

            ctx.SaveChanges();
        }
    }
}
