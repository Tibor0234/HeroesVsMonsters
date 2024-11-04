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

            if (new string[] {"s", "a", "b", "c"}.Contains(category.ToLower()))
                return true;
            else
                return false;
        }
        public static bool ValidateLevel(string level, bool append)
        {
            if (level.IsNullOrEmpty() && !append)
                return true;

            if (new string[] { "dragon", "golem", "daemon", "vampire" }.Contains(level.ToLower()))
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
            for (int i = 0; i < abilitiesSplitted.Length; i++)
            {
                if (!HeroAbilities.ListOfAbilities.Select(a => a.ToLower()).Contains(abilitiesSplitted[i].ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
