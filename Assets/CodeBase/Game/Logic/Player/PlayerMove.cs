using Game.Logic.InteractiveObject;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMove : ObjectMove
    {
        public PlayerMove(Rigidbody2D body, ObjectStats stats) : base(body, stats)
        {
        }
    }
}