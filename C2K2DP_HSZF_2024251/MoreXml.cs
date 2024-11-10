using C2K2DP_HSZF_2024251.Application;
using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251
{
    public interface IMoreXml
    {
        public void ChooseMethod();
        public void ChooseEntity(bool method);
        public void Xml(bool method, int entity);
    }
    public class MoreXml : IMoreXml
    {
        IHeroesVsMonstersDbContext ctx;
        IMoreXmlService moreXmlService;

        public MoreXml(IHeroesVsMonstersDbContext ctx, IMoreXmlService moreXmlService)
        {
            this.ctx = ctx;
            this.moreXmlService = moreXmlService;
        }

        public void ChooseMethod()
        {
            Console.Clear();
            Console.WriteLine("Choose method:\n\nImport Battles [I]\t\t\tExport Entities [E]");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.I)
                Xml(true, 3);
            else if (keyInfo.Key == ConsoleKey.E)
                ChooseEntity(false);
        }

        public void ChooseEntity(bool method)
        {
            Console.WriteLine("\nChoose entity:\n\nHero [H]\t\t\tMonster [M]\t\t\tBattle [B]");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.H)
                Xml(method, 1);
            else if (keyInfo.Key == ConsoleKey.M)
                Xml(method, 2);
            else if (keyInfo.Key == ConsoleKey.B)
                Xml(method, 3);
        }

        public void Xml(bool method, int entity)
        {
            if (method)
            {
                bool imported = false;
                switch (entity)
                {
                    case 1:
                        imported = moreXmlService.ImportEntities<Hero>();
                        break;
                    case 2:
                        imported = moreXmlService.ImportEntities<Monster>();
                        break;
                    case 3:
                        imported = moreXmlService.ImportEntities<Battle>();
                        break;
                }
                if (imported)
                {
                    Console.Clear();
                    Console.WriteLine("The targeted file was successfully imported into the database.");
                }   
                else
                {
                    Console.Clear();
                    Console.WriteLine("The targeted file could not be found, you need to export before trying to import.");
                }
            }
            else
            {
                switch (entity)
                {
                    case 1:
                        moreXmlService.ExportEntities<Hero>();
                        break;
                    case 2:
                        moreXmlService.ExportEntities<Monster>();
                        break;
                    case 3:
                        moreXmlService.ExportEntities<Battle>();
                        break;
                }
                Console.Clear();
                Console.WriteLine("The table was successfully exported to Xml.");
            }
            Console.ReadKey();
        }
    }
}
