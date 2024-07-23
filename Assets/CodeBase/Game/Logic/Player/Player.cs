using Game.Logic.InteractiveObject;
using Game.Logic.Player.Animation;
using Game.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private UnitAnimationExtension _animation;
        [SerializeField] private Rigidbody2D _body;

        private AnimationFsm _animationFsm;
        private PlayerInput _playerInput;
        private PlayerMove _playerMove;
        private DamageHandler _damageHandler;
        private ShootHandler _weapon;

        private ObjectStats _playerStats;
        private Vector3 _standartScale;
        private bool _onAttck = false;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(AnimationFsm animationFsm,
            PlayerInput playerInput, ShootHandler weapon,
            ObjectStats stats)
        {
            _animationFsm = animationFsm;
            _playerInput = playerInput;
            _weapon = weapon;
            _playerStats = stats;
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

            _weapon.InvokeCanShoot += OnCanAttack;
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
            if (_onAttck)
                return;
            _playerMove.BlockMove = true;
            _onAttck = true;
            _animationFsm.SetState<AttackState>(true, AttackAnimationEnd);
            _weapon.Shoot(_playerInput.MousePosition());
        }

        private void OnCanAttack()
        {
            _onAttck = false;
        }

        private void AttackAnimationEnd()
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

            _weapon.InvokeCanShoot -= OnCanAttack;
        }
    }
}