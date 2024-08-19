using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {

        private Vector3 _standartScale;
        private UnitAnimationWrapper _animation;

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

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}