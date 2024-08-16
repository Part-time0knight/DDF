using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;
using UnityEngine;


namespace Game.Logic.Player.PlayerFsm.States
{
    public class Run : Hitable
    {
        private readonly PlayerShootHandler _playerShoot;
        private readonly UnitAnimationWrapper _animation;
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _playerMove;
        private readonly Transform _transform;

        private Vector3 _standartScale;

        public Run(IGameStateMachine stateMachine, PlayerInput playerInput,
            UnitAnimationWrapper animation, PlayerMoveHandler playerMove,
            Rigidbody2D body, PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerShootHandler playerShoot) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _animation = animation;
            _playerMove = playerMove;
            _transform = body.transform;
            _playerShoot = playerShoot;
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
            _playerInput.InvokeMoveHorizontal += OnMoveHorizontal;

            _playerShoot.StartAutomatic();

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

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveButtonsUp -= OnMoveEnd;
            _playerInput.InvokeMove -= Move;
            _playerInput.InvokeMoveHorizontal -= OnMoveHorizontal;
            _playerShoot.StopAutomatic();
        }


    }
}