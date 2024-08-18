using Game.Logic.Handlers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMoveHandler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;
        
        public BulletMoveHandler(Rigidbody2D body, BulletSettngs stats, IPauseHandler pauseHandler) : base(body, stats, pauseHandler)
        {

            _filter.useTriggers = true;
        }

        protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(Vector2.zero, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);

            return speedMultiplier;
        }

        [Serializable]
        public class BulletSettngs : Settings
        { }
    }
}