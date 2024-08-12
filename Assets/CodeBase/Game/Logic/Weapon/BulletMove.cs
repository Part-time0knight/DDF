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

        public override void Move(Vector2 speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;
            Velocity = speedMultiplier * _stats.CurrentSpeed;
        }

        public void CollisionCheck()
        {
            if (_isStoped)
                return;

            _body.Cast(_speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);
        }

        [Serializable]
        public class BulletSettngs : Settings
        {

        }
    }
}