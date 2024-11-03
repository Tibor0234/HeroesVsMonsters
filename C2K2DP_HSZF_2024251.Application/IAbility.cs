using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IAbility
    {
        string Name { get; set; }
        void Execute();
    }

    public class LightningStrike : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class ArcaneBlast : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class ShadowBolt : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class Stealth : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class Fireball : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class FlameShield : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class PalmStrike : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }

    public class VineWhip : IAbility
    {
        public string Name { get; set; }
        public void Execute() { }
    }
}
