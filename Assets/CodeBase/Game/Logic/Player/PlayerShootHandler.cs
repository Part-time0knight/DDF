using Cysharp.Threading.Tasks;
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
        private readonly PlayerInput _playerInput;

        private bool _breakAutomatic = false;
        private Vector2 _target = Vector2.left;


        public PlayerShootHandler(Bullet.Pool bulletPool, 
            PlayerSettings settings, IPauseHandler pauseHandler,
            Transform weaponPoint, PlayerInput playerInput) : base(bulletPool, settings, pauseHandler)
        {
            _weapon = weaponPoint;
            _settings.Owner = Tags.Player;
            _playerInput = playerInput;
        }

        public override void Initialize()
        {
            base.Initialize();
            _playerInput.InvokeMove += UpdateTarget;
        }

        public override void Dispose()
        {
            base.Dispose();
            _playerInput.InvokeMove -= UpdateTarget;
        }

        private void UpdateTarget(Vector2 direction)
        {
            if (direction != Vector2.zero)
                _target = direction + (Vector2)_weapon.position;
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
                    Shoot(_weapon.position, _target);
            } while (!_breakAutomatic);
        }


        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}