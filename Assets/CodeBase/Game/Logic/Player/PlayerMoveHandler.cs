using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {
        public PlayerMoveHandler(Rigidbody2D body, PlayerSettings stats, IPauseHandler pauseHandler) : base(body, stats, pauseHandler)
        { }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}