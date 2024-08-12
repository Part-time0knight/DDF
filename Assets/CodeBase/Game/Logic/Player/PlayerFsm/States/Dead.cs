using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Dead : Hitable
    {
        private readonly UnitAnimationExtension _animation;

        public Dead(IGameStateMachine stateMachine,
            UnitAnimationExtension animation,
            PlayerDamageHandler.PlayerSettings damageSettings) : base(stateMachine, damageSettings)
        {
            _animation = animation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animation.PlayAnimation(AnimationNames.DEATH);
        }

        protected override void OnHit()
        {
            if (_damageSettings.CurrentHits <= 0)
                return;
            _stateMachine.Enter<Idle>();
        }
    }
}
