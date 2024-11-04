using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class More_Xml
    {
        public static void ExportEntities(HeroesVsMonstersDbContext ctx)
        {
            List<Hero> heroes = ctx.Heroes.ToList();
            List<Monster> monsters = ctx.Monsters.ToList();
            XmlSerializer heroSerializer = new XmlSerializer(typeof(List<Hero>));
            XmlSerializer monsterSerializer = new XmlSerializer(typeof(List<Hero>));


            using (StreamWriter writer = new StreamWriter("heroes.xml"))
            {
                heroSerializer.Serialize(writer, heroes);
            }

            using (StreamWriter writer = new StreamWriter("monsters.xml"))
            {
                monsterSerializer.Serialize(writer, monsters);
            }

            Console.WriteLine("Szééééép");
        }
    }
}
