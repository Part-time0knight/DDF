using Game.Logic.Player.Animation;

public interface IAnimationStatesFactory
{
    TState Create<TState>()
        where TState : class, IAnimationState;
}
