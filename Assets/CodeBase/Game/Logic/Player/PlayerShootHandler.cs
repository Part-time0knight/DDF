using Game.Logic.Weapon;
using Installers;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler
    {
        protected override Transform WeapontPoint { get; set; }

        public PlayerShootHandler(Bullet.Pool bulletPool, PlayerSettings settings,
            Transform weaponPoint) : base(bulletPool, settings)
        {
            WeapontPoint = weaponPoint;
        }

        [Serializable]
        public class PlayerSettings : Settings
        {

        }
    }
}