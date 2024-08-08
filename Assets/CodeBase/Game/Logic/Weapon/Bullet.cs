using Game.Logic.InteractiveObject;
using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet, GameObject> InvokeHit;

        protected Vector2 direction = Vector2.zero;
        protected BulletMove _bulletMove;

        protected virtual void Awake()
        {
            _bulletMove.InvokeCollision += OnHit;
        }

        protected virtual void Initialize(Vector2 startPos, Vector2 targetPos)
        {
            transform.position = startPos;
            direction = (targetPos - startPos).normalized;
            _bulletMove.Move(direction);
        }

        [Inject]
        private void Construct(BulletMove bulletMove)
        {
            _bulletMove = bulletMove;
        }

        private void FixedUpdate()
        {
            _bulletMove.CollisionCheck();
        }

        private void OnHit(GameObject objectHit)
        {
            if (objectHit.tag == "Player")
                return;

            Debug.Log(objectHit.name);
            InvokeHit?.Invoke(this, objectHit);
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Bullet>
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
            protected override void Reinitialize(Vector2 startPos, Vector2 targetPos, Bullet item)
            {
                base.Reinitialize(startPos, targetPos, item);
                item.Initialize(startPos, targetPos);
            }
        }
    }
}