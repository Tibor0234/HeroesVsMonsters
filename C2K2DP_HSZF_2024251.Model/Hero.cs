using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Quic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Model
{
    public class Hero : IEntity
    {
        public Hero() { }

        public Hero(string name, string category, int strength, int speed, string abilities)
        {
            Name = name;
            Category = category;
            Strength = strength;
            Speed = speed;
            Abilities = abilities;
            Battles = new List<Battle>();
            MaxHealth = 400;
            MaxManna = 100;
        }

        private int manna;

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Category { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        [StringLength(500)]
        [NotMapped]
        public int Health { get; set; }
        [NotMapped]
        private int MaxHealth { get; set; }
        [NotMapped]
        public int Manna { get { return manna <= 100 ? manna : 100; } set { manna = value; } }
        [NotMapped]
        public int MaxManna { get; set; }
        public string Abilities { get; set; }
        [NotMapped]
        public ICollection<Battle> Battles { get; set; }

        public override string ToString()
        {
            return $"{Id,2}. {Name,-17} {Category,-4} st: {Strength,-4} sp: {Speed,-4} {Abilities}";
        }

        public string ToStringInBattle()
        {
            return $"{Name,-17} {Category,-4} st: {Strength,-4} sp: {Speed,-4} ({Health} Hp)";
        }

        public void BattleInit()
        {
            Health = MaxHealth;
            Manna = 0;
        }
    }
}
