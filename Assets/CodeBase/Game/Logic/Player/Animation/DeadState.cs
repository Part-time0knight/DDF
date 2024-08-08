using Game.Logic.StaticData;
using System;

namespace Game.Logic.Player.Animation
{
    public class DeadState : AnimationState
    {
        public DeadState(AnimationFsm fsm, UnitAnimationExtension animation) : base(fsm, animation)
        {
        }

        public override void Enter(Action callback)
        {
            Animation.PlayAnimation(AnimationNames.DEATH, null);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}