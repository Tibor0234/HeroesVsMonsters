using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Application
{
    public interface IAbility
    {
        void Execute();
    }

    public class LightningStrike : IAbility
    {
        public void Execute() { }
    }

    public class ArcaneBlast : IAbility
    {
        public void Execute() { }
    }

    public class ShadowBolt : IAbility
    {
        public void Execute() { }
    }

    public class Stealth : IAbility
    {
        public void Execute() { }
    }

    public class Fireball : IAbility
    {
        public void Execute() { }
    }

    public class FlameShield : IAbility
    {
        public void Execute() { }
    }

    public class PalmStrike : IAbility
    {
        public void Execute() { }
    }

    public class VineWhip : IAbility
    {
        public void Execute() { }
    }
}
