using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.InteractiveObject
{
    public class ObjectMove
    {
        protected Vector2 Velocity 
        {
            get => _body.velocity;
            set
            {
                _body.velocity = value;
            }

        }

        protected readonly Rigidbody2D _body;
        protected Settings _stats;
        protected bool _isStoped;
        protected ContactFilter2D _filter;
        protected List<RaycastHit2D> _raycasts = new();
        protected float _collisionOffset;
        protected Vector2 _resultSpeed;

        public ObjectMove(Rigidbody2D body, Settings stats)
        {
            _body = body;
            _isStoped = false;
            _filter = new ContactFilter2D();
            _stats = stats;
            _collisionOffset = 0.1f;
            _stats.CurrentSpeed = _stats.Speed;
        }

        public virtual void Move(Vector2 speedMultiplier)
        {
            if (_isStoped)
                return;

            _body.Cast(speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            _resultSpeed = speedMultiplier;

            foreach (var cast in _raycasts)
            {
                _resultSpeed.x = cast.normal.x != 0 ? 0 : _resultSpeed.x;
                _resultSpeed.y = cast.normal.y != 0 ? 0 : _resultSpeed.y;
            }
            Velocity = _resultSpeed * _stats.CurrentSpeed;
        }

        public void Stop()
            => _body.velocity = Vector2.zero;

        public class Settings
        {
            [field: SerializeField] public float Speed { get; private set; }
            public float CurrentSpeed { get; set; }
        }
    }
}