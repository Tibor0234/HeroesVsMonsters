using C2K2DP_HSZF_2024251.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IAppendOrModifyEntityService
    {
        public string[] GetHeroProperties(bool append);
        public string[] GetMonsterProperties(bool append);
        public string GetAbilities(string abilities);
        public void SetHeroProperties(Hero hero, string[] properties);
        public void SetMonsterProperties(Monster monster, string[] properties);
    }
    public class AppendOrModifyEntityService : IAppendOrModifyEntityService
    {
        IValidation validation;
        IHeroAbilities heroAbilities;
        public AppendOrModifyEntityService(IValidation validation, IHeroAbilities heroAbilities)
        {
            this.validation = validation;
            this.heroAbilities = heroAbilities;
        }
        public string[] GetHeroProperties(bool append)
        {
            string[] properties = new string[5];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            if (!validation.ValidateName(properties[0], append))
                return ["failed"];
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            if (!validation.ValidateCategory(properties[1], append))
                return ["failed"];
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            if (!validation.ValidateStrengthAndSpeed(properties[2], append))
                return ["failed"];
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            if (!validation.ValidateStrengthAndSpeed(properties[3], append))
                return ["failed"];
            Console.SetCursorPosition(0, 11);
            Console.WriteLine("List Of Abilities: " + heroAbilities.AllAbilities);

            Console.SetCursorPosition(0, 9);
            properties[4] = Console.ReadLine();
            if (!validation.ValidateAbilities(properties[4], append))
                return ["failed"];

            return properties;
        }
        public string[] GetMonsterProperties(bool append)
        {
            string[] properties = new string[4];
            Console.SetCursorPosition(0, 1);
            properties[0] = Console.ReadLine();
            if (!validation.ValidateName(properties[0], append))
                return ["failed"];
            Console.SetCursorPosition(0, 3);
            properties[1] = Console.ReadLine();
            if (!validation.ValidateLevel(properties[1], append))
                return ["failed"];
            Console.SetCursorPosition(0, 5);
            properties[2] = Console.ReadLine();
            if (!validation.ValidateStrengthAndSpeed(properties[2], append))
                return ["failed"];
            Console.SetCursorPosition(0, 7);
            properties[3] = Console.ReadLine();
            if (!validation.ValidateStrengthAndSpeed(properties[3], append))
                return ["failed"];

            return properties;
        }

        public string GetAbilities(string abilities)
        {
            string[] abilityList = heroAbilities.ListOfAbilities.Where(a => abilities.ToLower().Contains(a.ToLower())).ToArray();
            string abilityString = "";
            foreach (string ability in abilityList)
            {
                abilityString += ", " + ability;
            }
            return abilityString.Substring(2);
        }

        public void SetHeroProperties(Hero hero, string[] properties)
        {
            if (!properties[0].IsNullOrEmpty())
                hero.Name = properties[0];
            if (!properties[1].IsNullOrEmpty())
                hero.Category = properties[1];
            if (!properties[2].IsNullOrEmpty())
                hero.Strength = int.Parse(properties[2]);
            if (!properties[3].IsNullOrEmpty())
                hero.Speed = int.Parse(properties[3]);
            if (!properties[4].IsNullOrEmpty())
                hero.Abilities = GetAbilities(properties[4]);
        }

        public void SetMonsterProperties(Monster monster, string[] properties)
        {
            if (!properties[0].IsNullOrEmpty())
                monster.Name = properties[0];
            if (!properties[1].IsNullOrEmpty())
                monster.Level = properties[1];
            if (!properties[2].IsNullOrEmpty())
                monster.Strength = int.Parse(properties[2]);
            if (!properties[3].IsNullOrEmpty())
                monster.Speed = int.Parse(properties[3]);
        }
    }
}
