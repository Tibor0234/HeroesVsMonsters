using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace C2K2DP_HSZF_2024251.Model
{
    public class Battle : IHasId
    {
        public Battle() { }
        public Battle(Hero hero, Monster monster, DateTime date, string result)
        {
            Hero = hero;
            HeroId = hero.Id;
            HeroName = hero.Name;
            Monster = monster;
            MonsterId = monster.Id;
            MonsterName = monster.Name;
            Date = date;
            Result = result;
        }
        [Key]
        public int Id { get; set; }
        [NotMapped]
        [XmlIgnore]
        [ForeignKey("HeroId")]
        public Hero Hero { get; set; }
        public int HeroId { get; set; }
        public string HeroName { get; set; }
        [NotMapped]
        [XmlIgnore]
        [ForeignKey("MonsterId")]
        public Monster Monster { get; set; }
        public int MonsterId { get; set; }
        public string MonsterName { get; set; }
        public DateTime Date { get; set; }
        public string Result { get; set; }

        public override string ToString()
        {
            return $"{Id,2}. {DateOnly.FromDateTime(Date).ToString(),-13} {Hero.Name,-18} vs  {Monster.Name,-18} :  {Result}";
        }
    }
}
