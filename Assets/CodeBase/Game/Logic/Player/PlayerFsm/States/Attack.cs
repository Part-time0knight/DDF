using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Attack : Hitable
    {
        private readonly UnitAnimationExtension _animation;
        private readonly PlayerInput _playerInput;
        private PlayerShootHandler _shootHandler;

        public Attack(IGameStateMachine stateMachine, PlayerInput playerInput,
        UnitAnimationExtension animation, PlayerShootHandler shootHandler,
        PlayerDamageHandler.PlayerSettings damageSettings) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _shootHandler = shootHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _shootHandler.Shoot(_playerInput.MousePosition());
            _animation.PlayAnimation(AnimationNames.ATTACK + AnimationNames.MAGIC, OnAttackEnd);
        }

        private void OnAttackEnd()
        {
            if (_playerInput.IsMoveButtonPress)
                _stateMachine.Enter<Run>();
            else
                _stateMachine.Enter<Idle>();
        }
    }
}