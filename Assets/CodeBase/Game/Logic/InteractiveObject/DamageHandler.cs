
using UnityEngine;

namespace Game.Logic.InteractiveObject
{
    public abstract class DamageHandler
    {
        private Settings _stats;

        public DamageHandler(Settings stats) 
        {
            _stats = stats;
            _stats.CurrentHits = _stats.HitPoints;
        }

        public void TakeDamage(int damage)
            => _stats.CurrentHits -= damage;

        public class Settings
        {
            [field: SerializeField] public int HitPoints { get; private set; }
            public int CurrentHits { get; set; }
        }
    }
}