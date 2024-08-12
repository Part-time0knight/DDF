using Core.Infrastructure.GameFsm;
using Game.Logic.Player.Animation;
using Game.Logic.StaticData;

namespace Game.Logic.Player.PlayerFsm.States
{
    public class Dead : Hitable
    {
        private readonly UnitAnimationWrapper _animation;

        public Dead(IGameStateMachine stateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings, 
            UnitAnimationWrapper animation) : base(stateMachine, damageSettings)
        {
            _animation = animation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animation.PlayAnimation(AnimationNames.Death);
        }

        protected override void OnHit()
        {
            if (_damageSettings.CurrentHits <= 0)
                return;
            _stateMachine.Enter<Idle>();
        }
    }
}
