using Game.Logic.InteractiveObject;
using Game.Logic.Player.Animation;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private UnitAnimationExtension _animation;
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private ObjectStats _playerStats = new();

        private AnimationFsm _animationFsm;
        private PlayerInput _playerInput;
        private PlayerMove _playerMove;
        private DamageHandler _damageHandler;

        private Vector3 _standartScale;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(AnimationFsm animationFsm, PlayerInput playerInput)
        {
            _animationFsm = animationFsm;
            _playerInput = playerInput;

        }

        private void Start()
        {
            _playerMove = new(_body, _playerStats);

            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerInput.InvokeMoveButtonsUp += OnMoveEnd;
            _playerInput.InvokeMoveHorizontal += OnMoveHorizontal;
            _playerInput.InvokeMove += OnMove;
            _playerInput.InvokeAttackButton += OnAttack;

            _animationFsm.AddState(new IdleState(_animationFsm, _animation));
            _animationFsm.AddState(new RunState(_animationFsm, _animation));
            _animationFsm.AddState(new AttackState(_animationFsm, _animation));
            _animationFsm.SetState<IdleState>(true);

            _standartScale = new(
                transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);

            _damageHandler = new(_playerStats);
        }

        private void OnMoveBegin()
        {
            _animationFsm.SetState<RunState>(true);
        }

        private void OnMoveEnd()
        {
            _animationFsm.SetState<IdleState>(true);
        }

        private void OnMoveHorizontal(float direction)
        {
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(direction),
                _standartScale.y, _standartScale.z);
            transform.localScale = scale;
        }

        private void OnMove(Vector2 direction)
        {
            _playerMove.Move(direction);
        }

        private void OnAttack()
        {
            _playerMove.BlockMove = true;
            _animationFsm.SetState<AttackState>(true, AttackEnd);
        }

        private void AttackEnd()
        {
            _playerMove.BlockMove = false;
            if (_playerInput.IsMoveButtonPress)
                _animationFsm.SetState<RunState>();

        }

        private void OnDestroy()
        {
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
            _playerInput.InvokeMoveButtonsUp -= OnMoveEnd;
            _playerInput.InvokeMoveHorizontal -= OnMoveHorizontal;
            _playerInput.InvokeAttackButton -= OnAttack;
            _playerInput.InvokeMove -= OnMove;
        }
    }
}