using Game.Logic.Player.Animation;
using Zenject;

public class AnimationStatesFactory : IAnimationStatesFactory
{
    private readonly DiContainer _container;

    public AnimationStatesFactory(DiContainer container) =>
        _container = container;

    public TState Create<TState>() where TState : class, IAnimationState
    {
        TState state = _container.Instantiate<TState>();
        return state;
    }
}
