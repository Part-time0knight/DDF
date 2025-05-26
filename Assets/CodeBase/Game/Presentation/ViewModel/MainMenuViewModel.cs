using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.MainMenu;
using Game.Presentation.View;
using System;
using UnityEngine;

namespace Game.Presentation.ViewModel
{
    public class MainMenuViewModel : AbstractViewModel
    {
        private readonly IGameStateMachine _gameFsm;

        protected override Type Window => typeof(MainMenuView);

        public MainMenuViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameFsm) : base(windowFsm)
        {
            _gameFsm = gameFsm;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void InvokeChooseHeroes()
        {

        }

        public void InvokeStartPlay()
        {

        }

        public void InvokeExit()
        {
            _gameFsm.Enter<Exit>();
        }
    }
}