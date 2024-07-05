using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using UnityEngine;
using Zenject;

namespace Game.Test
{
    public class TestPlayerMove : MonoBehaviour
    {
        [SerializeField] private SPUM_Prefabs _animation;

        private AnimationFsm _animationFsm;


        [Inject]
        private void Construct(AnimationFsm animationFsm)
        {
            _animationFsm = animationFsm;
        }

        private void Start()
        {
            _animationFsm.AddState(new IdleState(_animationFsm, _animation));
            _animationFsm.AddState(new RunState(_animationFsm, _animation));
            _animationFsm.SetState<IdleState>();
        }
    }
}