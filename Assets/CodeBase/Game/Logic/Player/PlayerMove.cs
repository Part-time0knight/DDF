using Game.Logic.InteractiveObject;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMove : ObjectMove
    {
        public PlayerMove(Rigidbody2D body, PlayerSettings stats, IPauseHandler pauseHandler) : base(body, stats, pauseHandler)
        { }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}