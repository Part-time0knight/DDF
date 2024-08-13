using Core.Infrastructure.GameFsm;
using Game.Infrastructure.Signals;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using Zenject;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Attack : Hitable
    {
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerShootHandler _shootHandler;
        private readonly SignalBus _signalBus;

        public Attack(IGameStateMachine stateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings, PlayerInput playerInput,
            UnitAnimationWrapper animation, PlayerShootHandler shootHandler, SignalBus signalBus) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _shootHandler = shootHandler;
            _signalBus = signalBus;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _shootHandler.Shoot(_playerInput.MousePosition());
            _signalBus.Subscribe<PauseSignal>(OnPause);
            _animation.PlayAnimation(AnimationNames.Attack + AnimationNames.Magic, OnAttackEnd);
        }

        private void OnAttackEnd()
        {
            _signalBus.Unsubscribe<PauseSignal>(OnPause);
            if (_playerInput.IsMoveButtonPress)
                _stateMachine.Enter<Run>();
            else
                _stateMachine.Enter<Idle>();
        }

        private void OnPause(PauseSignal signal)
        {
            if (signal.IsPause)
                _animation.AnimationSpeed(0f);
            else
                _animation.AnimationSpeed(1f);
        }
    }
}