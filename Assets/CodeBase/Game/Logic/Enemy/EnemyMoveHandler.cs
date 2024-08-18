using Game.Logic.Handlers;
using Game.Logic.Player;
using System;
using UnityEngine;

namespace Game.Logic.Enemy
{
    public class EnemyMoveHandler : MoveHandler
    {
        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;
        private Vector2 _playerDirection;

        public EnemyMoveHandler(Rigidbody2D body, EnemySettings stats,
            IPauseHandler pause, PlayerMoveHandler.PlayerSettings playerSettings)
            : base(body, stats, pause)
        {
            _playerSettings = playerSettings;
            _playerDirection = Vector2.zero;
        }

        public void MoveToPlayer()
        {
            _playerDirection = (_playerSettings.CurrentPosition - _stats.CurrentPosition).normalized;
            Move(_playerDirection);
        }

        [Serializable]
        public class EnemySettings : Settings
        {

        }
    }
}