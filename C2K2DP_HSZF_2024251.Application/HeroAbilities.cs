using C2K2DP_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using System.Reflection;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IHeroAbilities
    {
        public string[] ListOfAbilities { get; }
        public string AllAbilities { get; }
    }
    public class HeroAbilities : IHeroAbilities
    {
        public string[] ListOfAbilities { get { 
            return Assembly.GetExecutingAssembly().GetTypes()
                            .Where(t => typeof(IAbility).IsAssignableFrom(t) && !t.IsInterface)
                            .Select(t => t.Name)
                            .ToArray();
            } }
        public string AllAbilities
        {
            get
            {
                string abilities = "";
                foreach (var ability in ListOfAbilities)
                {
                    abilities += ", " + ability;
                }
                return abilities.Substring(2);
            } }
        public void UseAbility()
        {
            
        }
        public void LoadAbilities()
        {

        }
    }
}
