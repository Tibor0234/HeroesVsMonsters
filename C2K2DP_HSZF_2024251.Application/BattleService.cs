using C2K2DP_HSZF_2024251.Model;
using C2K2DP_HSZF_2024251.Persistence.MsSql;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public static class BattleService
    {
        public static bool round;
        public static bool Round { get { round = !round; return round; } }
        public static bool BattleEnded { get; set; }
        public static int DrawOpponent()
        {
            Random rnd = new Random();
            return rnd.Next(60, 80);
        }
        public static int GetOpponentId(HeroesVsMonstersDbContext ctx, int draw)
        {
            if (draw % ctx.Monsters.Count() == 0)
            {
                return ctx.Monsters.Count();
            }
            else
            {
                return draw % ctx.Monsters.Count();
            }
        }
        public static Hero FindHero(HeroesVsMonstersDbContext ctx, int heroId) => ctx.Heroes.Find(heroId);
        public static Monster FindMonster(HeroesVsMonstersDbContext ctx, int monsterId) => ctx.Monsters.Find(monsterId);
        public static void BattleInitialization(Hero hero, Monster monster)
        {
            hero.BattleInit();
            monster.BattleInit();
        }
        public static bool BattleOn(Hero hero, Monster monster)
        {
            if (hero.Health > 0 && monster.Health > 0)
                return true;
            else if (!BattleEnded)
            {
                BattleEnded = true;
                if (hero.Health <= 0)
                    hero.Health = 0;
                else
                    monster.Health = 0;
                return true;
            }
            else
                return false;
        }
        public static void Attack(IEntity attacker, IEntity attacked)
        {
            Random rnd = new Random();
            int Attack = rnd.Next(attacker.Strength - 30, attacker.Strength + 30);
            int Defense = (int)rnd.Next(attacked.Speed - 30, attacked.Speed + 30) % 2;
            int Damage = Attack - Defense;
            if (Damage < 1)
                Damage = 1;
            else if (Damage > 100)
                Damage = 100;

            attacked.Health -= Damage;

            if (attacker is Hero)
            {
                Hero attackerHero = attacker as Hero;
                attackerHero.Manna += Damage;
            }
        }
        public static bool HeroWon(HeroesVsMonstersDbContext ctx, Hero hero, Monster monster)
        {
            string result;
            bool value;
            if (monster.Health <= 0)
            {
                result = "Hero won";
                value = true;
            }
            else
            {
                result = "Monster won";
                value = false;
            }
            Battle battle = new Battle(hero, monster, DateTime.Now, result);
            hero.Battles.Add(battle);
            monster.Battles.Add(battle);
            ctx.Battles.Add(battle);
            ctx.SaveChanges();
            round = false;
            BattleEnded = false;
            return value;
        }
    }
}
