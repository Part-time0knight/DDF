using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using UnityEngine.Playables;
using Zenject;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Attack : Hitable
    {
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerShootHandler _shootHandler;

        public Attack(IGameStateMachine stateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerInput playerInput,
            UnitAnimationWrapper animation,
            PlayerShootHandler shootHandler) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _shootHandler = shootHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _shootHandler.Shoot(_playerInput.MousePosition());
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