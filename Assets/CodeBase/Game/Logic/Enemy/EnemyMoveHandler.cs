using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Logic.StaticData;
using System;
using UnityEngine;

namespace Game.Logic.Enemy
{
    public class EnemyMoveHandler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;

        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;
        private readonly Animator _animator;
        private Vector2 _playerDirection;
        private Vector3 _standartScale;
        private bool _blockX;
        private bool _blockY;

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettingsHandler stats,
            IPauseHandler pause,
            PlayerMoveHandler.PlayerSettings playerSettings,
            Animator animator)
            : base(body, stats.MoveSettings, pause)
        {
            _playerSettings = playerSettings;
            _playerDirection = Vector2.zero;
            _animator = animator;
            _standartScale = new(
                _animator.transform.localScale.x,
                _animator.transform.localScale.y,
                _animator.transform.localScale.z);
        }

        public override void Move(Vector2 speedMultiplier)
        {
            base.Move(speedMultiplier);
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(speedMultiplier.x),
                _standartScale.y, _standartScale.z);
            _animator.transform.localScale = scale;
        }

        public void MoveToPlayer()
        {
            _playerDirection = (_playerSettings.CurrentPosition - _body.position).normalized;
            Move(_playerDirection);
        }

        protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                if(hit.transform.tag != Tags.Enemy)
                    InvokeCollision?.Invoke(hit.transform.gameObject);
                else
                {
                    _blockX = hit.normal.x != 0;
                    _blockY = hit.normal.y != 0;
                    speedMultiplier.x = _blockX ? 0 : speedMultiplier.x;
                    speedMultiplier.y = _blockY ? 0 : speedMultiplier.y;
                }
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