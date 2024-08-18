using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMoveHandler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;

        private Vector2 _speedMultiplier = Vector2.zero;


        public BulletMoveHandler(Rigidbody2D body, BulletSettngs stats, IPauseHandler pauseHandler) : base(body, stats, pauseHandler)
        {
        }

        protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(_speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);
            return speedMultiplier;
        }

        [Serializable]
        public class BulletSettngs : Settings
        { }
    }
}