using Game.Logic.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class AttackState : AnimationState
    {
        public AttackState(AnimationFsm fsm, UnitAnimationExtension animation) : base(fsm, animation)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter attack");
            Animation.PlayAnimation(AnimationNames.ATTACK + AnimationNames.MAGIC, AttackCallback);
            //Animation._anim.
        }

        public override void Exit()
        {
            Debug.Log("Exit attack");
        }

        public override void Update()
        {
            
        }

        private void AttackCallback()
        {
            Fsm.SetState<IdleState>();
        }
    }
}