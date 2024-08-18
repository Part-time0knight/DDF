using Cysharp.Threading.Tasks;
using Game.Logic.Enemy;
using Game.Logic.Handlers;
using Game.Logic.StaticData;
using Game.Logic.Weapon;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerShootHandler : ShootHandler
    {
        private readonly Transform _weapon;
        private readonly EnemyMoveHandler.EnemySettings _enemySettings;

        private bool _breakAutomatic = false;

        public PlayerShootHandler(Bullet.Pool bulletPool, 
            PlayerSettings settings,
            IPauseHandler pauseHandler,
            Transform weaponPoint,
            EnemyMoveHandler.EnemySettings enemySettings) : base(bulletPool, settings, pauseHandler)
        {
            _weapon = weaponPoint;
            _settings.Owner = Tags.Player;
            _enemySettings = enemySettings;

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Dispose()
        {
            base.Dispose();
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
                if (!_breakAutomatic)
                    Shoot(_weapon.position, _enemySettings.CurrentPosition);
            } while (!_breakAutomatic);
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}