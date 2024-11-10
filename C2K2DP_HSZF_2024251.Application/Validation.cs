using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IValidation
    {
        public bool ValidateName(string name, bool append);
        public bool ValidateCategory(string category, bool append);
        public bool ValidateLevel(string level, bool append);
        public bool ValidateStrengthAndSpeed(string strengthOrSpeed, bool append);
        public bool ValidateAbilities(string abilities, bool append);
    }
    public class Validation : IValidation
    {
        IHeroAbilities heroAbilities;
        public Validation(IHeroAbilities heroAbilities)
        {
            this.heroAbilities = heroAbilities;
        }
        public bool ValidateName(string name, bool append)
        {
            if (name.IsNullOrEmpty() && !append)
                return true;

            if (!name.IsNullOrEmpty())
                return true;
            else
                return false;
        }
        public bool ValidateCategory(string category, bool append)
        {
            if (category.IsNullOrEmpty() && !append)
                return true;

            if (new string[] {"s", "a", "b", "c"}.Contains(category.ToLower()))
                return true;
            else
                return false;
        }
        public bool ValidateLevel(string level, bool append)
        {
            if (level.IsNullOrEmpty() && !append)
                return true;

            if (new string[] { "dragon", "golem", "daemon", "vampire" }.Contains(level.ToLower()))
                return true;
            else
                return false;
        }
        public bool ValidateStrengthAndSpeed(string strengthOrSpeed, bool append)
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
        public bool ValidateAbilities(string abilities, bool append)
        {
            if (abilities.IsNullOrEmpty() && !append)
                return true;

            string[] abilitiesSplitted = abilities.Split(", ");
            for (int i = 0; i < abilitiesSplitted.Length; i++)
            {
                if (!heroAbilities.ListOfAbilities.Select(a => a.ToLower()).Contains(abilitiesSplitted[i].ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
