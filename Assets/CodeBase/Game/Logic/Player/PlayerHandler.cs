using Game.Logic.Player.Animation;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        private UnitAnimationExtension _animation;

        private AnimationFsm _animationFsm;
        private PlayerInput _playerInput;
        private PlayerMove _playerMove;
        private PlayerDamageHandler _damageHandler;
        private PlayerShootHandler _weapon;

        private PlayerShootHandler.PlayerSettings _playerSettings;

        private Vector3 _standartScale;
        private bool _onAttck = false;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(AnimationFsm animationFsm,
            PlayerInput playerInput, PlayerShootHandler weapon,
            PlayerMove playerMove, PlayerDamageHandler damageHandler,
            UnitAnimationExtension animation, 
            PlayerShootHandler.PlayerSettings playerSettings)
        {
            _animationFsm = animationFsm;
            _playerInput = playerInput;
            _weapon = weapon;
            _playerMove = playerMove;
            _damageHandler = damageHandler;
            _animation = animation;
            _playerSettings = playerSettings;
        }

        private void Start()
        {
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

            _playerSettings.InvokeCanShoot += OnCanAttack;
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

            _playerSettings.InvokeCanShoot -= OnCanAttack;
        }
    }
}