using Game.Logic.InteractiveObject;
using System;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMove : ObjectMove
    {
        public override bool BlockMove
        {
            get => _isStoped;
            set
            {
                _isStoped = value;

                if (_speedMultiplier == Vector2.zero)
                    return;

                _body.velocity = _isStoped == true ? 
                    Vector2.zero : _speedMultiplier * Stats.Speed;
            }
        }

        public Action<GameObject> InvokeCollision;

        private Vector2 _speedMultiplier = Vector2.zero;


        public BulletMove(Rigidbody2D body, ObjectStats stats) : base(body, stats)
        {
        }

        public override void Move(Vector2 speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;
            Velocity = speedMultiplier * Stats.Speed;
        }

        public void CollisionCheck()
        {
            if (_isStoped)
                return;

            _body.Cast(_speedMultiplier, _filter, _raycasts, Stats.Speed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);
        }
    }
}