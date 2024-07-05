using Game.Logic.StaticData;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class IdleState : AnimationState
    {
        public IdleState(AnimationFsm fsm, SPUM_Prefabs animation) : base(fsm, animation)
        {

        }

        public override void Enter()
        {
            Animation.PlayAnimation(AnimationNames.IDLE);
            Debug.Log("Enter Idle state");
        }

        public override void Exit()
        {
            Debug.Log("Exit Idle state");
        }

        public override void Update()
        {
            Debug.Log("Update Idle state");
            if (Input.GetButtonDown("Vertical")
                || Input.GetButtonDown("Horizontal"))
                Fsm.SetState<RunState>();
        }
    }
}