using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public class MoveHandler : IPauseble, IInitializable, IDisposable
    {
        protected Vector2 Velocity 
        {
            get => _body.velocity;
            set => _body.velocity = value;

        }

        protected readonly Rigidbody2D _body;
        protected readonly Settings _stats;
        protected readonly IPauseHandler _pause;

        protected ContactFilter2D _filter;
        protected List<RaycastHit2D> _raycasts;
        protected float _collisionOffset;
        protected Vector2 _pausedVelocity;
        protected bool _paused;

        public MoveHandler(Rigidbody2D body, Settings stats, IPauseHandler pause)
        {
            _body = body;
            _stats = stats;
            _pause = pause;

            _filter = new();
            _raycasts = new();

            _collisionOffset = 0f;
            _stats.CurrentSpeed = _stats.Speed;
            _stats.CurrentPosition = _body.position;
            _paused = false;
        }

        public void Initialize()
        {
            _pause.SubscribeElement(this);
        }

        public void Dispose()
        {
            _pause.UnsubscribeElement(this);
        }

        public virtual void Move(Vector2 speedMultiplier)
        { 
            Velocity = CollisionCheck(speedMultiplier) *
            _stats.CurrentSpeed * PauseSpeed();
            _stats.CurrentPosition = _body.position;
        }


        public void Stop()
            => _body.velocity = Vector2.zero;

        public virtual void OnPause(bool active)
        {
            _paused = active;
            if (_paused )
                Velocity = Vector2.zero;
        }

        protected virtual float PauseSpeed()
            => _paused == false ? 1f : 0f;

        protected virtual Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);
            foreach (var cast in _raycasts)
            {
                speedMultiplier.x = cast.normal.x != 0 ? 0 : speedMultiplier.x;
                speedMultiplier.y = cast.normal.y != 0 ? 0 : speedMultiplier.y;
            }
            return speedMultiplier;
        }

        public class Settings
        {
            [field: SerializeField] public float Speed { get; protected set; }
            public float CurrentSpeed { get; set; }

            public Vector2 CurrentPosition { get; set; }

            public Settings()
            { }

            public Settings(float speed, float currentSpeed, Vector2 currentPosition)
            {
                Speed = speed;
                CurrentSpeed = currentSpeed;
                CurrentPosition = currentPosition;
            }

            public Settings(Settings settings) : this(
                settings.Speed,
                settings.CurrentSpeed,
                settings.CurrentPosition)
            { }
        }
    }
}