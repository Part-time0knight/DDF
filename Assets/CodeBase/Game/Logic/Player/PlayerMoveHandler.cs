using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {

        private Vector3 _standartScale;
        private UnitAnimationWrapper _animation;
        private float _speedMod;

        public PlayerMoveHandler(Rigidbody2D body,
            PlayerSettings stats,
            IPauseHandler pauseHandler,
            UnitAnimationWrapper animation) : base(body, stats, pauseHandler)
        {
            _animation = animation;
            _standartScale = new(
                _animation.transform.localScale.x,
                _animation.transform.localScale.y,
                _animation.transform.localScale.z);
        }

        public override void Move(Vector2 speedMultiplier)
        {
            base.Move(speedMultiplier);
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(speedMultiplier.x),
                _standartScale.y, _standartScale.z);
            _animation.transform.localScale = scale;
        }

        protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);
            foreach (var cast in _raycasts)
            {
                if (cast.transform.tag == Tags.Enemy)
                    _speedMod = 0.5f;
                else
                    _speedMod = 0;
                speedMultiplier.x = cast.normal.x != 0 ? speedMultiplier.x * _speedMod : speedMultiplier.x;
                speedMultiplier.y = cast.normal.y != 0 ? speedMultiplier.x * _speedMod : speedMultiplier.y;
            }
            return speedMultiplier;
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}