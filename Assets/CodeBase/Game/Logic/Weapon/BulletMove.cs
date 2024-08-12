using Game.Logic.InteractiveObject;
using System;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMove : ObjectMove
    {
        public Action<GameObject> InvokeCollision;

        private Vector2 _speedMultiplier = Vector2.zero;


        public BulletMove(Rigidbody2D body, BulletSettngs stats) : base(body, stats)
        {
            _stats = stats;
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
        {

        }
    }
}