using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Model
{
    public class Monster : IEntity
    {
        public Monster() { }

        public Monster(string name, string level, int strength, int speed)
        {
            Name = name;
            Level = level;
            Strength = strength;
            Speed = speed;
            Battles = new List<Battle>();
            MaxHealth = 500;
        }
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Level { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        [NotMapped]
        public int Health { get; set; }
        private int MaxHealth { get; set; }
        [NotMapped]
        public ICollection<Battle> Battles { get; set; }

        public override string ToString()
        {
            return $"{Id,2}. {Name,-15} {Level,-10} st: {Strength,-4} sp: {Speed,-4}";
        }

        public string ToStringInBattle()
        {
            return $"{Name,-15} {Level,-10} st: {Strength,-4} sp: {Speed,-4} ({Health} Hp)";
        }

        public void BattleInit()
        {
            Health = MaxHealth;
        }
    }
}
