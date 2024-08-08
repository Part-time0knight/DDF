using Game.Logic.Player.Animation;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        private AnimationFsm _animationFsm;
        private PlayerInput _playerInput;
        private PlayerMove _playerMove;
        private PlayerDamageHandler _damageHandler;
        private PlayerShootHandler _weapon;

        private PlayerShootHandler.PlayerSettings _shootSettings;
        private PlayerDamageHandler.PlayerSettings _damageHandlerSettings;

        private Vector3 _standartScale;
        private bool _onAttck = false;
        private bool _onDead = false;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(AnimationFsm animationFsm,
            PlayerInput playerInput, PlayerShootHandler weapon,
            PlayerMove playerMove, PlayerDamageHandler damageHandler,
            PlayerShootHandler.PlayerSettings shootSettings, PlayerDamageHandler.PlayerSettings damageHandlerSettings)
        {
            _animationFsm = animationFsm;
            _playerInput = playerInput;
            _weapon = weapon;
            _playerMove = playerMove;
            _damageHandler = damageHandler;
            _shootSettings = shootSettings;
            _damageHandlerSettings = damageHandlerSettings;
        }

        private void Start()
        {
            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerInput.InvokeMoveButtonsUp += OnMoveEnd;
            _playerInput.InvokeMoveHorizontal += OnMoveHorizontal;
            _playerInput.InvokeMove += OnMove;
            _playerInput.InvokeAttackButton += OnAttack;

            _animationFsm.AddState<IdleState>();
            _animationFsm.AddState<RunState>();
            _animationFsm.AddState<AttackState>();
            _animationFsm.AddState<DeadState>();
            _animationFsm.SetState<IdleState>(true);

            _standartScale = new(
                transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);

            _shootSettings.InvokeCanShoot += OnCanAttack;
            _damageHandlerSettings.InvokeHitPointsChange += OnDead;
            _damageHandlerSettings.InvokeHitPointsChange += OnResurrect;
        }

        private void OnMoveBegin()
        {
            if (_onDead)
                return;
            _animationFsm.SetState<RunState>(true);
        }

        private void OnMoveEnd()
        {
            if (_onDead)
                return;
            _animationFsm.SetState<IdleState>(true);
        }

        private void OnMoveHorizontal(float direction)
        {
            if (_onDead)
                return;
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(direction),
                _standartScale.y, _standartScale.z);
            transform.localScale = scale;
        }

        private void OnMove(Vector2 direction)
        {
            if (_onDead)
                return;
            _playerMove.Move(direction);
        }

        private void OnAttack()
        {
            if (_onAttck || _onDead)
                return;
            _playerMove.Block = true;
            _onAttck = true;
            _animationFsm.SetState<AttackState>(true, AttackAnimationEnd);
            _weapon.Shoot(_playerInput.MousePosition());
        }

        private void OnCanAttack()
        {
            _onAttck = false;
        }

        private void OnDead()
        {
            if (_damageHandlerSettings.CurrentHits > 0)
                return;
            _animationFsm.SetState<DeadState>(true);
            _onDead = true;
        }

        private void OnResurrect()
        {
            if (_damageHandlerSettings.CurrentHits <= 0 || !_onDead)
                return;
            _animationFsm.SetState<IdleState>(true);
            _onDead = false;
        }

        private void AttackAnimationEnd()
        {
            _playerMove.Block = false;
            
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

            _shootSettings.InvokeCanShoot -= OnCanAttack;
            _damageHandlerSettings.InvokeHitPointsChange -= OnDead;
            _damageHandlerSettings.InvokeHitPointsChange -= OnResurrect;
        }
    }
}