using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using UnityEngine;


namespace Game.Logic.Player.PlayerFsm.States
{
    public class Run : Hitable
    {
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerMove _playerMove;
        private readonly Transform _transform;

        private Vector3 _standartScale;

        public Run(IGameStateMachine stateMachine, PlayerInput playerInput,
            UnitAnimationWrapper animation, PlayerMove playerMove,
            Rigidbody2D body, PlayerDamageHandler.PlayerSettings damageSettings) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _playerMove = playerMove;
            _transform = body.transform;
            _standartScale = new(
                _transform.localScale.x,
                _transform.localScale.y,
                _transform.localScale.z);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsUp += OnMoveEnd;
            _playerInput.InvokeMove += Move;
            _playerInput.InvokeAttackButton += OnAttack;
            _playerInput.InvokeMoveHorizontal += OnMoveHorizontal;
            _animation.PlayAnimation(AnimationNames.Run);

        }

        private void OnMoveHorizontal(float direction)
        {
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(direction),
                _standartScale.y, _standartScale.z);
            _transform.localScale = scale;
        }

        private void Move(Vector2 direction)
        {
            _playerMove.Move(direction);
        }

        private void OnMoveEnd()
        {
            _playerMove.Stop();
            _stateMachine.Enter<Idle>();

        }

        private void OnAttack()
        {
            _playerMove.Stop();
            _stateMachine.Enter<Attack>();

        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveButtonsUp -= OnMoveEnd;
            _playerInput.InvokeMove -= Move;
            _playerInput.InvokeAttackButton -= OnAttack;
            _playerInput.InvokeMoveHorizontal -= OnMoveHorizontal;
        }


    }
}