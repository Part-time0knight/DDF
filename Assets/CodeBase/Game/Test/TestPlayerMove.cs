using Game.Logic.Player;
using Game.Logic.Player.Animation;
using UnityEngine;
using Zenject;

namespace Game.Test
{
    public class TestPlayerMove : MonoBehaviour
    {
        [SerializeField] private UnitAnimationExtension _animation;

        private AnimationFsm _animationFsm;
        private PlayerInput _playerInput;

        private Vector3 _standartScale;

        [Inject]
        private void Construct(AnimationFsm animationFsm, PlayerInput playerInput)
        {
            _animationFsm = animationFsm;
            _playerInput = playerInput;

        }

        private void Start()
        {
            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerInput.InvokeMoveButtonsUp += OnMoveEnd;
            _playerInput.InvokeMoveHorizontal += OnMoveHorizontal;
            _playerInput.InvokeAttackButton += OnAttack;
            _animationFsm.AddState(new IdleState(_animationFsm, _animation));
            _animationFsm.AddState(new RunState(_animationFsm, _animation));
            _animationFsm.AddState(new AttackState(_animationFsm, _animation));
            _animationFsm.SetState<IdleState>();
            _standartScale =new(
                _animation._anim.transform.localScale.x,
                _animation._anim.transform.localScale.y,
                _animation._anim.transform.localScale.z);
        }

        private void OnMoveBegin()
        {
            _animationFsm.SetState<RunState>();
        }

        private void OnMoveEnd()
        {
            _animationFsm.SetState<IdleState>();
        }

        private void OnMoveHorizontal(float direction)
        {
            if (direction == 0f)
                return;
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(direction),
                _standartScale.y, _standartScale.z);
            _animation._anim.transform.localScale = scale;
        }

        private void OnAttack()
        {
            _animationFsm.SetState<AttackState>();
        }

        private void OnDestroy()
        {
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
            _playerInput.InvokeMoveButtonsUp -= OnMoveEnd;
            _playerInput.InvokeMoveHorizontal -= OnMoveHorizontal;
            _playerInput.InvokeAttackButton -= OnAttack;
        }
    }
}