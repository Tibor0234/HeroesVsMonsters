using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IMoreXmlService
    {
        public void ExportEntities<TClass>() where TClass : class, IHasId;
        public bool ImportEntities<TClass>() where TClass : class, IHasId;
    }
    public class MoreXmlService : IMoreXmlService
    {
        IHeroesVsMonstersDbContext ctx;

        public MoreXmlService(IHeroesVsMonstersDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void ExportEntities<TClass>() where TClass : class, IHasId
        {
            var dbSetProperty = ctx.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<TClass>));
            var dbSet = (DbSet<TClass>)dbSetProperty.GetValue(ctx);

            List<TClass> entities = dbSet.ToList();
            XmlSerializer serializer = new XmlSerializer(typeof(List<TClass>));
            Directory.CreateDirectory("Xml");
            string path = Path.Combine("Xml", $"{dbSetProperty.Name}.xml");
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, entities);
            }
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }

        public bool ImportEntities<TClass>() where TClass : class, IHasId
        {
            var dbSetProperty = ctx.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<TClass>));
            var dbSet = (DbSet<TClass>)dbSetProperty.GetValue(ctx);

            XmlSerializer serializer = new XmlSerializer(typeof(List<TClass>));

            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("Xml", $"{dbSetProperty.Name}.xml")))
                {
                    List<TClass> entities = (List<TClass>)serializer.Deserialize(reader);
                    entities.ForEach(e => e.Id = 0);
                    dbSet.RemoveRange(dbSet.ToArray());
                    dbSet.AddRange(entities);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
