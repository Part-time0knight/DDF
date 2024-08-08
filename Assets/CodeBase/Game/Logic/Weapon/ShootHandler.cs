using Game.Logic.InteractiveObject;
using Game.Logic.Player;
using System;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Logic.Weapon
{
    public abstract class ShootHandler
    {
        protected readonly Bullet.Pool _bulletPool;
        protected readonly Settings _settings;

        protected List<Bullet> _bullets = new();
        protected Timer _timer = new();
        protected bool _onLoad = false;
        protected Bullet _currentBullet;

        protected abstract Transform WeapontPoint { get; set; }

        public ShootHandler(Bullet.Pool bulletPool, Settings settings)
        {
            _bulletPool = bulletPool;
            _settings = settings;
            _settings.CurrentAttackDelay = _settings.AttackDelay;
            _settings.CurrentDamage = _settings.Damage;
            _settings.TimeToAttack = 0;
        }

        public virtual void Shoot(Vector2 target)
        {
            _currentBullet = _bulletPool.Spawn(WeapontPoint.position, target);
            _bullets.Add(_currentBullet);

            _currentBullet.InvokeHit += Hit;

            _onLoad = true;

            _timer.Initialize(_settings.CurrentAttackDelay, (tick) => 
            {
                _settings.TimeToAttack = _settings.CurrentAttackDelay - tick;
                _settings.InvokeTimeUpdate?.Invoke();
            }, () => 
            { 
                _onLoad = false;
                _settings.InvokeCanShoot?.Invoke();
            });
            _timer.Play();
        }

        private void Hit(Bullet bullet, GameObject target)
        {
            bullet.InvokeHit -= Hit;
            _bulletPool.Despawn(bullet);
            _bullets.Remove(bullet);
        }

        [Serializable]
        public class Settings
        {
            /// <summary>
            /// Called when the cooldown timer ends. 
            /// </summary>
            public Action InvokeCanShoot;
            /// <summary>
            /// Called when the cooldown timer ticks. 
            /// </summary>
            public Action InvokeTimeUpdate;

            [field: SerializeField] public float AttackDelay { get; private set; }
            [field: SerializeField] public float Damage { get; private set; }

            public float CurrentAttackDelay { get; set; }
            public float CurrentDamage { get; set; }
            public float TimeToAttack { get; set; }
        }

    }
}

