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
    public static class HeroAbilities
    {
        public static string[] ListOfAbilities { get { 
            return Assembly.GetExecutingAssembly().GetTypes()
                            .Where(t => typeof(IAbility).IsAssignableFrom(t) && !t.IsInterface)
                            .Select(t => t.Name)
                            .ToArray();
            } }
        public static void UseAbility()
        {
            
        }
        public static void LoadAbilities()
        {

        }
    }
}
