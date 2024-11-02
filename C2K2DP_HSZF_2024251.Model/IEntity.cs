using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Model
{
    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        int Strength { get; set; }
        int Speed { get; set; }
        int Health { get; set; }
        ICollection<Battle> Battles { get; set; }
        public void BattleInit();
    }
}
