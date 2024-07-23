using Game.Logic.InteractiveObject;
using Game.Logic.Misc;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public class ShootHandler
    {
        protected readonly Bullet.Pool _bulletPool;
        protected readonly Transform _weapon;

        protected List<Bullet> _bullets = new();
        protected Timer _timer = new();
        protected ObjectStats _stats;

        protected bool _onLoad = false;

        public ShootHandler(Bullet.Pool bulletPool, Transform weapon, ObjectStats stats)
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

            _bullets.Add(_bulletPool.Spawn(target));
            _onLoad = true;
            _timer.Initialize(_stats.AttackDelay, () => _onLoad = false);
            _timer.Play();
        }
    }
}