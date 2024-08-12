using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.InteractiveObject;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Idle : Hitable
    {
        private readonly UnitAnimationExtension _animation;
        private readonly PlayerInput _playerInput;

        public Idle(IGameStateMachine stateMachine,
            PlayerInput playerInput, UnitAnimationExtension animation,
            PlayerDamageHandler.PlayerSettings damageSettings) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerInput.InvokeAttackButton += OnAttack;
            _animation.PlayAnimation(AnimationNames.IDLE);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
            _playerInput.InvokeAttackButton -= OnAttack;
        }

        private void OnMoveBegin()
        {
            _stateMachine.Enter<Run>();
        }

        private void OnAttack()
        {
            _stateMachine.Enter<Attack>();
        }




    }
}