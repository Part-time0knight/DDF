using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Weapon
{
    public abstract class ShootHandler
    {

        protected readonly Bullet.Pool _bulletPool;
        protected readonly Settings _settings;

        protected List<Bullet> _bullets = new();
        protected Timer _timer = new();
        protected Bullet _currentBullet;

        protected abstract Transform WeapontPoint { get; set; }

        public ShootHandler(Bullet.Pool bulletPool, Settings settings)
        {
            _bulletPool = bulletPool;
            _settings = settings;
            _settings.CurrentAttackDelay = _settings.AttackDelay;
            _settings.CurrentDamage = _settings.Damage;
            _settings.CanShoot = true;
            _timer.Initialize(0.1f, null);
            _timer.Play();
        }

        public virtual void Shoot(Vector2 target)
        {
            _currentBullet = _bulletPool.Spawn(WeapontPoint.position, target);
            _bullets.Add(_currentBullet);
            _settings.InvokeShot?.Invoke();
            _settings.CanShoot = false;
            _currentBullet.InvokeHit += Hit;
            _timer.Initialize(_settings.CurrentAttackDelay, 0.05f, () => 
            { 
                _settings.CanShoot = true;
                //_settings.InvokeCanShoot?.Invoke();
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
            /// Called when handler make a shot. 
            /// </summary>
            public Action InvokeShot;

            [field: SerializeField] public float AttackDelay { get; private set; }
            [field: SerializeField] public float Damage { get; private set; }

            public float CurrentAttackDelay { get; set; }
            public float CurrentDamage { get; set; }

            public bool CanShoot { get; set; }
        }

    }
}

