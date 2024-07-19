using UnityEngine;
using Zenject;

namespace Game.Logic.Misc
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _body;
        [SerializeField] private float _speed = 10;

        private Vector2 direction = Vector2.zero;
        private BulletMove _bulletMove;

        private void Awake()
        {
            _bulletMove = new(_body, _speed);
        }

        private void Initialize(Vector2 startPoint, Vector2 clickPos)
        {
            transform.localPosition = startPoint;
            direction = clickPos.normalized;
        }

        private void FixedUpdate()
        {

        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, Bullet>
        {

            protected override void Reinitialize(Vector2 startPoint, Vector2 endPoint, Bullet item)
            {
                base.Reinitialize(startPoint, endPoint, item);
                item.Initialize(startPoint, endPoint);
            }
        }
    }
}