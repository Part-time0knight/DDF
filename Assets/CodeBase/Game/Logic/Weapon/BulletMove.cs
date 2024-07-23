using Game.Logic.InteractiveObject;
using UnityEngine;

namespace Game.Logic.Misc
{
    public class BulletMove : ObjectMove
    {
        public BulletMove(Rigidbody2D body, ObjectStats stats) : base(body, stats)
        {
        }

        public override void Move(Vector2 speedMultiplier)
        {
            Velocity = speedMultiplier * Stats.Speed;
        }
    }
}