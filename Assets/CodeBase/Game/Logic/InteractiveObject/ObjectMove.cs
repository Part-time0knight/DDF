using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.InteractiveObject
{
    public class ObjectMove
    {
        public virtual bool BlockMove 
        {
            get => _isStoped;
            set 
            {
                _body.velocity = Vector2.zero;
                _isStoped = value;
            }
        }

        protected Vector2 Velocity 
        {
            get => _body.velocity;
            set
            {
                _body.velocity = value;
            }

        }

        protected ObjectStats Stats;

        protected readonly Rigidbody2D _body;

        protected bool _isStoped;
        protected ContactFilter2D _filter;
        protected List<RaycastHit2D> _raycasts = new();
        protected float _collisionOffset;
        protected Vector2 _resultSpeed;

        public ObjectMove(Rigidbody2D body, ObjectStats stats)
        {
            _body = body;
            _isStoped = false;
            _filter = new ContactFilter2D();
            Stats = stats;
            _collisionOffset = 0.1f;
        }

        public virtual void Move(Vector2 speedMultiplier)
        {
            if (_isStoped)
                return;

            _body.Cast(speedMultiplier, _filter, _raycasts, Stats.Speed * Time.fixedDeltaTime + _collisionOffset);

            _resultSpeed = speedMultiplier;

            foreach (var cast in _raycasts)
            {
                _resultSpeed.x = cast.normal.x != 0 ? 0 : _resultSpeed.x;
                _resultSpeed.y = cast.normal.y != 0 ? 0 : _resultSpeed.y;
            }
            Velocity = _resultSpeed * Stats.Speed;
        }

        public void Stop()
            => _body.velocity = Vector2.zero;
    }
}