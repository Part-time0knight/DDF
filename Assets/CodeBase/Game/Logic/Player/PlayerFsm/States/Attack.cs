using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using UnityEngine;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Attack : Hitable
    {
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerShootHandler _shootHandler;
        private readonly PlayerShootHandler.PlayerSettings _shootSettings;
        private readonly Timer _timer;

        public Attack(IGameStateMachine stateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerInput playerInput,
            UnitAnimationWrapper animation,
            PlayerShootHandler shootHandler, PlayerShootHandler.PlayerSettings shootSettings) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _shootHandler = shootHandler;
            _shootSettings = shootSettings;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //_shootHandler.Shoot(Vector2.left, null);
            OnAttackEnd();
            //_animation.PlayAnimation(AnimationNames.Attack + AnimationNames.Magic, OnAttackEnd);
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