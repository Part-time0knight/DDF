using Core.Infrastructure.GameFsm.States;
using Game.Logic.Misc;
using UnityEngine;

namespace Game.Infrastructure.States.MainMenu
{
    public class Exit : IState
    {
        private readonly Timer _timer = new();

        public void OnEnter()
        {
            _timer.Initialize(0.5f, InvokeQuit).Play();
        }

        public void OnExit()
        {
        }

        private void InvokeQuit()
        {
            Application.Quit();
        }
    }
}