using Game.Logic.InteractiveObject;
using Game.Logic.Misc;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Weapon
{
    public class ShootHandler
    {
        /// <summary>
        /// Called when the cooldown timer ends. 
        /// </summary>
        public event Action InvokeCanShoot;

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
            if (_onLoad)
            {
                Debug.Log("Weapon on reload!");
                return;
            }
            _currentBullet = _bulletPool.Spawn(target);
            _bullets.Add(_currentBullet);
            _currentBullet.transform.SetParent(_weapon, false);

            _onLoad = true;
            _timer.Initialize(_stats.AttackDelay, () => 
            { 
                _onLoad = false;
                Debug.Log("end reload");
                InvokeCanShoot?.Invoke();
            });
            _timer.Play();
        }
    }
}

