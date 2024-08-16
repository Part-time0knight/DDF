using Game.Logic.Handlers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public abstract class ShootHandler : IPauseble, IInitializable, IDisposable
    {

        protected readonly Bullet.Pool _bulletPool;
        protected readonly Settings _settings;
        protected readonly IPauseHandler _pauseHandler;
        protected readonly Timer _timer = new();

        protected List<Bullet> _bullets = new();
        protected Bullet _currentBullet;

        public ShootHandler(Bullet.Pool bulletPool, Settings settings,
            IPauseHandler pauseHandler)
        {
            _bulletPool = bulletPool;
            _settings = settings;
            _pauseHandler = pauseHandler;

            _settings.CurrentAttackDelay = _settings.AttackDelay;
            _settings.CurrentDamage = _settings.Damage;
            _settings.CanShoot = true;
        }

        public virtual void Initialize()
        {
            _timer.Initialize(
                time: 0f, step: 0f, 
                callback: () => _pauseHandler.SubscribeElement(this))
                .Play();
            
        }

        public virtual void Dispose()
        {
            _pauseHandler.UnsubscribeElement(this);
        }

        public void OnPause(bool active)
        {
            if (_timer.Active)
            {
                if (active)
                    _timer.Pause();
                else
                    _timer.Play();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weponPos">World space position</param>
        /// <param name="target">World space position</param>
        /// <param name="onReloadEnd"></param>
        public virtual void Shoot(Vector2 weponPos, Vector2 target, Action onReloadEnd = null)
        {
            _currentBullet = _bulletPool
                .Spawn(weponPos, target, _settings.Owner);
            _bullets.Add(_currentBullet);
            _settings.InvokeShot?.Invoke();
            _settings.CanShoot = false;
            _currentBullet.InvokeHit += Hit;
            
            Action onEnd = () =>
            {
                _settings.CanShoot = true;
                onReloadEnd?.Invoke();
            };

            if (_timer.Active)
            {
                _timer.Stop();
                Debug.LogWarning(GetType().Name + " broken timer");
            }

            _timer.Initialize(
                _settings.CurrentAttackDelay, step: 0.05f, onEnd).Play();
        }

        public virtual void SetPause(bool active)
        {
            if (_timer.Active)
                if (active)
                    _timer.Pause();
                else
                    _timer.Play();

            foreach (var bullet in _bullets)
                bullet.SetPause(active);


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

            /// <summary>
            /// Tag of the GameObject that belongs to the owner of the ShootHandler.
            /// </summary>
            public string Owner { get; set; }
        }

    }
}

