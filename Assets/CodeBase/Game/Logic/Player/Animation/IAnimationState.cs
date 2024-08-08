using System;

namespace Game.Logic.Player.Animation
{
    public interface IAnimationState
    {
        void Enter(Action callback);

        void Exit();

        void Update();
    }
}