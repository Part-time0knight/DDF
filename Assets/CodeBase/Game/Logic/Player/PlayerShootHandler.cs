using Game.Logic.Handlers;
using Game.Logic.StaticData;
using Game.Logic.Weapon;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler
    {
        protected override Transform WeapontPoint { get; set; }

        public PlayerShootHandler(Bullet.Pool bulletPool, PlayerSettings settings, IPauseHandler pauseHandler,
            Transform weaponPoint) : base(bulletPool, settings, pauseHandler)
        {
            WeapontPoint = weaponPoint;
            _settings.Owner = Tags.Player;
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}