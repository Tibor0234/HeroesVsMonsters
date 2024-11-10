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
    public interface IBattleService
    {
        public bool Round { get; }
        public bool BattleEnded { get; set; }
        public int DrawOpponent();
        public int GetOpponentId(int draw);
        public Hero FindHero(int heroId);
        public Monster FindMonster(int monsterId);
        public void BattleInitialization(Hero hero, Monster monster);
        public bool BattleOn(Hero hero, Monster monster);
        public bool AbilityReady(Hero hero);
        public void Attack(IEntity attacker, IEntity attacked);
        public bool HeroWon(Hero hero, Monster monster);
    }
    public class BattleService : IBattleService
    {
        IHeroesVsMonstersDbContext ctx;
        public BattleService(IHeroesVsMonstersDbContext ctx)
        {
            this.ctx = ctx;
            round = false;
        }
        private bool round;
        public bool Round { get { round = !round; return round; } }
        public bool BattleEnded { get; set; }
        public int DrawOpponent()
        {
            Random rnd = new Random();
            return rnd.Next(60, 80);
        }
        public int GetOpponentId(int draw)
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
        public Hero FindHero(int heroId) => ctx.Heroes.Find(heroId);
        public Monster FindMonster(int monsterId) => ctx.Monsters.Find(monsterId);
        public void BattleInitialization(Hero hero, Monster monster)
        {
            hero.BattleInit();
            monster.BattleInit();
        }
        public bool BattleOn(Hero hero, Monster monster)
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
        public bool AbilityReady(Hero hero) => hero.Manna >= hero.MaxManna;
        public void Attack(IEntity attacker, IEntity attacked)
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
        public bool HeroWon(Hero hero, Monster monster)
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
