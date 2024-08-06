using Game.Logic.StaticData;
using System;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class RunState : AnimationState
    {
        public RunState(AnimationFsm fsm, UnitAnimationExtension animation) : base(fsm, animation)
        {
        }

        public override void Enter(Action callback)
        {
            Animation.PlayAnimation(AnimationNames.RUN, callback);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}