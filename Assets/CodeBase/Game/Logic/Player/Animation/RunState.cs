using Game.Logic.StaticData;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class RunState : AnimationState
    {
        public RunState(AnimationFsm fsm, SPUM_Prefabs animation) : base(fsm, animation)
        {
        }

        public override void Enter()
        {
            Animation.PlayAnimation(AnimationNames.RUN);
            Debug.Log("Enter Run state");
        }

        public override void Exit()
        {
            Debug.Log("Exit Run state");
        }

        public override void Update()
        {
            if (Input.GetButtonUp("Vertical")
                || Input.GetButtonUp("Horizontal"))
                Fsm.SetState<IdleState>();
        }
    }
}