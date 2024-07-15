using Game.Logic.StaticData;
using System;

namespace Game.Logic.Player.Animation
{
    public class AttackState : AnimationState
    {
        private Action _callback;

        public AttackState(AnimationFsm fsm, UnitAnimationExtension animation) : base(fsm, animation)
        {
        }

        public override void Enter(Action callback)
        {
            _callback = callback;
            Animation.PlayAnimation(AnimationNames.ATTACK + AnimationNames.MAGIC, AttackCallback);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }

        private void AttackCallback()
        {
            Fsm.SetState<IdleState>();
            _callback?.Invoke();
        }
    }
}