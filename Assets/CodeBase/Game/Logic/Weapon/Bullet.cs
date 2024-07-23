using Game.Logic.InteractiveObject;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Misc
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected ObjectStats _stats;

        protected Rigidbody2D _body;
        protected Vector2 direction = Vector2.zero;
        protected BulletMove _bulletMove;

        protected virtual void Awake()
        {
            _bulletMove = new(_body, _stats);
        }

        protected virtual void Initialize(Vector2 targetPos)
        {
            direction = targetPos.normalized;
            _bulletMove.Move(direction);
        }

        public class Pool : MonoMemoryPool<Vector2, Bullet>
        {

            protected override void Reinitialize(Vector2 targetPos, Bullet item)
            {
                base.Reinitialize(targetPos, item);
                item.Initialize(targetPos);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}