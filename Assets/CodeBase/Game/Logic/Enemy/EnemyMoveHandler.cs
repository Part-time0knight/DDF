using Game.Logic.Handlers;
using Game.Logic.Player;
using System;
using UnityEngine;

namespace Game.Logic.Enemy
{
    public class EnemyMoveHandler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;

        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;
        private Vector2 _playerDirection;

        public EnemyMoveHandler(Rigidbody2D body, EnemySettingsHandler stats,
            IPauseHandler pause, PlayerMoveHandler.PlayerSettings playerSettings)
            : base(body, stats.MoveSettings, pause)
        {
            _playerSettings = playerSettings;
            _playerDirection = Vector2.zero;
        }

        public override void Move(Vector2 speedMultiplier)
            => Velocity = CollisionCheck(speedMultiplier) *
                _stats.CurrentSpeed * PauseSpeed();

        public void MoveToPlayer()
        {
            _playerDirection = (_playerSettings.CurrentPosition - _body.position).normalized;
            Move(_playerDirection);
        }

        protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);
            return speedMultiplier;
        }

        [Serializable]
        public class EnemySettings : Settings
        {
            public EnemySettings(Settings settings) : base(settings)
            { }
        }
    }
}