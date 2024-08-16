using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Idle : Hitable
    {
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerShootHandler _playerShoot;


        public Idle(IGameStateMachine stateMachine,
            PlayerInput playerInput, UnitAnimationWrapper animation,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerShootHandler _shootHandler) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _playerShoot = _shootHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsDown += OnMoveBegin;
            _playerShoot.StartAutomatic();
            _animation.PlayAnimation(AnimationNames.Idle);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerShoot.StopAutomatic();
            _playerInput.InvokeMoveButtonsDown -= OnMoveBegin;
        }

        private void OnMoveBegin()
        {
            _stateMachine.Enter<Run>();
        }
    }
}