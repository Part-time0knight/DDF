using Game.Logic.InteractiveObject;
using Game.Logic.Misc;
using UnityEngine;
using Zenject;

namespace Game.Logic.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _body;
        [SerializeField] protected ObjectStats _stats;

        protected Vector2 direction = Vector2.zero;
        protected BulletMove _bulletMove;


        protected virtual void Awake()
        {
            _bulletMove = new(_body, _stats);
            
        }

        protected virtual void Initialize(Vector2 startPos, Vector2 targetPos)
        {
            transform.position = startPos;
            direction = (targetPos - startPos).normalized;
            _bulletMove.Move(direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

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
                base.OnCreated(item);
                item.transform.SetParent(_buffer);
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