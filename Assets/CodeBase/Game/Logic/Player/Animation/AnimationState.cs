
namespace Game.Logic.Player.Animation
{
    public abstract class AnimationState
    {

        protected readonly AnimationFsm Fsm;
        protected readonly SPUM_Prefabs Animation;

        public AnimationState(AnimationFsm fsm, SPUM_Prefabs animation)
        {
            Fsm = fsm;
            Animation = animation;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }
}