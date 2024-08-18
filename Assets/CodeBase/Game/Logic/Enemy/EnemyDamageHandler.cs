using Game.Logic.Handlers;
using System;

namespace Game.Logic.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public EnemyDamageHandler(EnemySettings stats, IPauseHandler pauseHandler) : base(stats, pauseHandler)
        { }

        [Serializable]
        public class EnemySettings : Settings
        { }
    }
}