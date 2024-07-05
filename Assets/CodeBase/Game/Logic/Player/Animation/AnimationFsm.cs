using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Logic.Player.Animation
{
    public class AnimationFsm : ITickable
    {
        private AnimationState _currentState;

        private Dictionary<Type, AnimationState> _states = new();

        public void AddState(AnimationState state)
        {
            if (_states.ContainsKey(state.GetType()))
                return;
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : AnimationState
        {
            Type type = typeof(T);

            if (!_states.ContainsKey(type) ||
                _states[type] == _currentState)
                return;

            _currentState?.Exit();
            _currentState = _states[type];
            _currentState.Enter();
        }

        public void Tick()
        {
            _currentState?.Update();
        }
    }
}