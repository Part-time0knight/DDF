using Game.Logic.InteractiveObject;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMove : ObjectMove
    {
        public BulletMove(Rigidbody2D body, float speed) : base(body)
        {
            Speed = speed;
        }
    }
}