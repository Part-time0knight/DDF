using System;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class DamageHandler
    {
        protected readonly Settings _stats;
        protected readonly IPauseHandler _pauseHandler;

        public DamageHandler(Settings stats, IPauseHandler pauseHandler) 
        {
            _stats = stats;
            _stats.CurrentHits = _stats.HitPoints;
            _pauseHandler = pauseHandler;
        }


        public void TakeDamage(int damage)
        {
            if (_pauseHandler.Active)
                return;
            _stats.CurrentHits -= damage;
            _stats.CurrentHits = Mathf.Max(Mathf.Min(_stats.CurrentHits, _stats.HitPoints), 0);
            _stats.InvokeHitPointsChange?.Invoke();
        }

        public class Settings
        {
            [field: SerializeField] public int HitPoints { get; private set; }
            public int CurrentHits { get; set; }

            public Action InvokeHitPointsChange;
        }
    }
}