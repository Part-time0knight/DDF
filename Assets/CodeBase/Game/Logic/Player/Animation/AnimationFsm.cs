using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Logic.Player.Animation
{
    public class AnimationFsm : ITickable
    {
        private readonly IAnimationStatesFactory _factory;



        private Dictionary<Type, IAnimationState> _states = new();

        private Action _callback;

        private IAnimationState _currentState;

        private IAnimationState _nextState;

        public AnimationFsm(IAnimationStatesFactory factory)
        {
            _factory = factory;
        }

        public void AddState<TState>() where TState : class, IAnimationState
        {
            if (_states.ContainsKey(typeof(TState)))
                return;
            var state = _factory.Create<TState>();
            _states.Add(state.GetType(), state);
        }


        /// <summary>
        /// Sets the state as the next one to change.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="change">if "change" is true, then the state will change now.</param>
        /// <param name="callback">A delegate called when the state exits.</param>
        public void SetState<T>(bool change = false , Action callback = null) where T : AnimationState
        {
            Type type = typeof(T);
            if (!_states.ContainsKey(type) ||
                    _states[type] == _currentState)
                return;
            _callback = callback;
            _nextState = _states[type];
            if (change)
                ChangeState();
        }

        /// <summary>
        /// Changes the current state to a prepared one.
        /// </summary>
        public void ChangeState()
        {
            if (_nextState == null)
                return;
            _currentState?.Exit();
            _currentState = _nextState;
            _nextState = null;
            var callback = _callback;
            _callback = null;
            _currentState.Enter(callback);
        }

        public void Tick()
        {
            _currentState?.Update();
        }
    }
}