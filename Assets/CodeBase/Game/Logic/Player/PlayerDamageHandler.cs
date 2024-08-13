using Game.Logic.InteractiveObject;
using System;

namespace Game.Logic.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        public PlayerDamageHandler(PlayerSettings stats) : base(stats)
        { }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}