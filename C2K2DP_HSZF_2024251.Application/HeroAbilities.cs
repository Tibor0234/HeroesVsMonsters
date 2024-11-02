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
    public class HeroAbilities
    {
        Hero hero;
        public HeroAbilities(Hero hero)
        {
            this.hero = hero;
        }
        public void UseAbility(int abilityNumber)
        {
            string[] ListOfAbilities = hero.Abilities.Split(", ");
            string abilityName = ListOfAbilities[abilityNumber - 1];
            MethodInfo method = typeof(HeroAbilities).GetMethod($"Ability_{abilityName}");
            method.Invoke(this,null);
            hero.Manna = 0;
        }
        public void Ability_LightningStrike()
        {
            Console.WriteLine("Ability used: LightningStrike");
        }
        public void Ability_ArcaneBlast()
        {
            Console.WriteLine("Ability used: ArcaneBlast");
        }
        public void Ability_ShadowBolt()
        {
            Console.WriteLine("Ability used: ShadowBolt");
        }
        public void Ability_Stealth()
        {
            Console.WriteLine("Ability used: Stealth");
        }
        public void Ability_Fireball()
        {
            Console.WriteLine("Ability used: Fireball");
        }
        public void Ability_FlameShield()
        {
            Console.WriteLine("Ability used: FlameShield");
        }
        public void Ability_PalmStrike()
        {
            Console.WriteLine("Ability used: PalmStrike");
        }
        public void Ability_VineWhip()
        {
            Console.WriteLine("Ability used: VineWhip");
        }
    }
}
