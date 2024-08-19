using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {

        private Vector3 _standartScale;

        public PlayerMoveHandler(Rigidbody2D body, PlayerSettings stats, IPauseHandler pauseHandler) : base(body, stats, pauseHandler)
        {
            _standartScale = new(
                _body.transform.localScale.x,
                _body.transform.localScale.y,
                _body.transform.localScale.z);
        }

        public override void Move(Vector2 speedMultiplier)
        {
            base.Move(speedMultiplier);
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(speedMultiplier.x),
                _standartScale.y, _standartScale.z);
            _body.transform.localScale = scale;
        }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}