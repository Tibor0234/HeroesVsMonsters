using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class Validation
    {
        public static bool ValidateName(string name, bool append)
        {
            if (name.IsNullOrEmpty() && !append)
                return true;

            if (!name.IsNullOrEmpty() && name.Length < 50)
                return true;
            else
                return false;
        }
        public static bool ValidateCategory(string category, bool append)
        {
            if (category.IsNullOrEmpty() && !append)
                return true;

            if (new string[] {"S", "A", "B", "C"}.Contains(category))
                return true;
            else
                return false;
        }
        public static bool ValidateLevel(string level, bool append)
        {
            if (level.IsNullOrEmpty() && !append)
                return true;

            if (new string[] { "Dragon", "Golem", "Daemon", "Vampire" }.Contains(level))
                return true;
            else
                return false;
        }
        public static bool ValidateStrengthAndSpeed(string strengthOrSpeed, bool append)
        {
            if (strengthOrSpeed.IsNullOrEmpty() && !append)
                return true;

            if (int.TryParse(strengthOrSpeed, out int value) && value > 0 && value <= 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValidateAbilities(string abilities, bool append)
        {
            if (abilities.IsNullOrEmpty() && !append)
                return true;

            string[] abilitiesSplitted = abilities.Split(", ");
            string[] allAbilities = typeof(HeroAbilities)
            .GetMethods()
            .Where(m => m.Name.Contains("Ability_"))
            .Select(m => m.Name.Substring(8))
            .ToArray();
            for (int i = 0; i < abilitiesSplitted.Length; i++)
            {
                if (!allAbilities.Contains(abilitiesSplitted[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
