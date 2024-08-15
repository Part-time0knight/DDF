using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet, GameObject> InvokeHit;

        protected Vector2 _direction = Vector2.zero;
        protected Vector2 _sampleDirection = Vector2.zero;
        protected BulletMove _bulletMove;
        protected string _owner;

        public void SetPause(bool active)
        {
            if (active)
                _direction = Vector2.zero;
            else
                _direction = _sampleDirection;
        }

        protected virtual void Awake()
        {
            _bulletMove.InvokeCollision += OnHit;
        }

        protected virtual void Initialize(Vector2 startPos, Vector2 targetPos, string owner)
        {
            transform.position = startPos;
            _direction = (targetPos - startPos).normalized;
            _sampleDirection = _direction;
            _owner = owner;
        }

        [Inject]
        private void Construct(BulletMove bulletMove)
        {
            _bulletMove = bulletMove;
        }

        private void FixedUpdate()
        {
            _bulletMove.Move(_direction);
        }

        private void OnHit(GameObject objectHit)
        {
            if (objectHit.tag == _owner)
                return;
            InvokeHit?.Invoke(this, objectHit);
        }

        private void OnDestroy()
        {
            _bulletMove.InvokeCollision -= OnHit;
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, string, Bullet>
        {

            protected Transform _buffer;

            [Inject]
            private void Construct(BulletBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(Bullet item)
            {
                item.transform.SetParent(_buffer);
                base.OnCreated(item);
            }

            /// <param name="startPos">World space position</param>
            /// <param name="targetPos">World space position</param>
            protected override void Reinitialize(Vector2 startPos, Vector2 targetPos, string owner, Bullet item)
            {
                base.Reinitialize(startPos, targetPos, owner, item);
                item.Initialize(startPos, targetPos, owner);
            }
        }
    }
}