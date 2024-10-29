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
    public class BattleService
    {
        HeroesVsMonstersDbContext ctx;
        Hero Hero { get; set; }
        Monster Monster { get; set; }
        bool round;
        public bool Round { get { round = !round; return round; } }
        public bool BattleEnded { get; set; }
        public BattleService(HeroesVsMonstersDbContext ctx)
        {
            this.ctx = ctx;
            this.round = false;
            BattleEnded = false;
        }
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
        public Hero FindHero(int heroId)
        {
            Hero = ctx.Heroes.Find(heroId);
            return Hero;
        }
        public Monster FindMonster(int monsterId)
        {
            Monster = ctx.Monsters.Find(monsterId);
            return Monster;
        }
        public void BattleInitialization()
        {
            Hero.BattleInit();
            Monster.BattleInit();
        }
        public bool BattleOn()
        {
            if (Hero.Health > 0 && Monster.Health > 0)
                return true;
            else if (!BattleEnded)
            {
                BattleEnded = true;
                if (Hero.Health <= 0)
                    Hero.Health = 0;
                else
                    Monster.Health = 0;
                return true;
            }
            else
                return false;
        }
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
        public bool HeroWon()
        {
            string result;
            bool value;
            if (Monster.Health <= 0)
            {
                result = "Hero won";
                value = true;
            }
            else
            {
                result = "Monster won";
                value = false;
            }
            Battle battle = new Battle(Hero, Monster, DateTime.Now, result);
            Hero.Battles.Add(battle);
            Monster.Battles.Add(battle);
            ctx.Battles.Add(battle);
            ctx.SaveChanges();
            round = false;
            BattleEnded = false;
            return value;
        }
    }
}
