using Cysharp.Threading.Tasks;
using Game.Logic.Handlers;
using Game.Logic.StaticData;
using Game.Logic.Weapon;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler
    {
        private Transform _weapon;
        private bool _breakAutomatic = false;

        public PlayerShootHandler(Bullet.Pool bulletPool, PlayerSettings settings, IPauseHandler pauseHandler,
            Transform weaponPoint) : base(bulletPool, settings, pauseHandler)
        {
            _weapon = weaponPoint;
            _settings.Owner = Tags.Player;
        }

        public void StartAutomatic()
        {
            _breakAutomatic = false;
            Repeater();
        }

        public void StopAutomatic()
        { 
            _breakAutomatic = true;
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active);
                Shoot(_weapon.position, Vector2.left);
            } while (!_breakAutomatic);
        }


        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}