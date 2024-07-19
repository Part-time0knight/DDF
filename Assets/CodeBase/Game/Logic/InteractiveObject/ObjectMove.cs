using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.InteractiveObject
{
    public class ObjectMove
    {
        public bool BlockMove 
        {
            get => _isStoped;
            set 
            {
                _body.velocity = Vector2.zero;
                _isStoped = value;
            }
        }

        protected virtual float Speed { get; set; }

        protected readonly Rigidbody2D _body;

        private bool _isStoped;
        private int _castCount;
        private ContactFilter2D _filter;
        private List<RaycastHit2D> _raycasts = new();
        private float _collisionOffset;
        private Vector2 _resultSpeed;

        public ObjectMove(Rigidbody2D body)
        {
            _body = body;
            _isStoped = false;
            _filter = new ContactFilter2D();
            _collisionOffset = 0.1f;
        }

        public void Move(Vector2 speedMultiplier)
        {
            if (_isStoped)
                return;

            

            _castCount = _body.Cast(speedMultiplier, _filter, _raycasts, Speed * Time.fixedDeltaTime + _collisionOffset);

            _resultSpeed = speedMultiplier;

            foreach (var cast in _raycasts)
            {
                _resultSpeed.x = cast.normal.x != 0 ? 0 : _resultSpeed.x;
                _resultSpeed.y = cast.normal.y != 0 ? 0 : _resultSpeed.y;
                Debug.Log(cast);
            }

            /*if (_castCount > 0)
            {
                Stop();
                return;
            }*/
            _body.velocity = _resultSpeed * Speed;
        }

        public void Stop()
            => _body.velocity = Vector2.zero;
    }
}