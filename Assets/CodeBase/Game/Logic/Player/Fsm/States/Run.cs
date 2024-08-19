using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using UnityEngine;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        private readonly PlayerShootHandler _playerShoot;
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _playerMove;

        public Run(IGameStateMachine stateMachine,
            PlayerInput playerInput,
            UnitAnimationWrapper animation,
            PlayerMoveHandler playerMove,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerShootHandler playerShoot) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _playerMove = playerMove;
            _playerShoot = playerShoot;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsUp += OnMoveEnd;
            _playerInput.InvokeMove += Move;

            _playerShoot.StartAutomatic();

            _animation.PlayAnimation(AnimationNames.Run);

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

        protected override void OnHit()
        {
            base.OnHit();
            _playerMove.Stop();
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveButtonsUp -= OnMoveEnd;
            _playerInput.InvokeMove -= Move;
            _playerShoot.StopAutomatic();
        }


    }
}