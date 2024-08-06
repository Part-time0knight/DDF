using Game.Logic.InteractiveObject;
using System;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Logic.Weapon
{
    public class ShootHandler
    {

        /// <summary>
        /// Called when the cooldown timer ends. 
        /// </summary>
        public event Action InvokeCanShoot;

        /// <summary>
        /// Called when the cooldown timer ticks. 
        /// </summary>
        public event Action InvokeReloadUpdate;

        protected readonly Bullet.Pool _bulletPool;
        protected readonly Transform _weapon;

        protected List<Bullet> _bullets = new();
        protected Timer _timer = new();
        protected ObjectStats _stats;

        protected bool _onLoad = false;
        protected Bullet _currentBullet;

        public ShootHandler(Bullet.Pool bulletPool, ObjectStats stats, Transform weapon)
        {
            _bulletPool = bulletPool;
            _weapon = weapon;
            _stats = stats;
        }

        public void Shoot(Vector2 target)
        {
            _currentBullet = _bulletPool.Spawn(_weapon.position, target);
            _bullets.Add(_currentBullet);

            _currentBullet.InvokeHit += Hit;

            _onLoad = true;

            _timer.Initialize(_stats.AttackDelay, (tick) => 
            {
                _stats.CurrentAttackDelay = _stats.AttackDelay - tick;
            }, () => 
            { 
                _onLoad = false;
                InvokeCanShoot?.Invoke();
            });
            _timer.Play();
        }

        private void Hit(Bullet bullet, GameObject target)
        {
            bullet.InvokeHit -= Hit;
            _bulletPool.Despawn(bullet);
            _bullets.Remove(bullet);
        }
    }
}

