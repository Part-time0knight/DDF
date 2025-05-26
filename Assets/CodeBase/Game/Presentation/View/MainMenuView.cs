using Core.MVVM.View;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class MainMenuView : AbstractPayloadView<MainMenuViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(MainMenuViewModel viewModel)
        {
            base.Construct(viewModel);
            _settings.ExitButton.onClick.AddListener(_viewModel.InvokeExit);
            _settings.PlayButton.onClick.AddListener(_viewModel.InvokeStartPlay);
        }

        protected override void OnDestroy()
        {
            _settings.ExitButton.onClick.RemoveListener(_viewModel.InvokeExit);
            _settings.PlayButton.onClick.RemoveListener(_viewModel.InvokeStartPlay);
            base.OnDestroy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button PlayButton { get; private set; }
            [field: SerializeField] public Button ExitButton { get; private set; }
        }
    }
}