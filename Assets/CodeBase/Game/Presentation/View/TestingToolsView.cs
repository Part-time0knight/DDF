using Core.MVVM.View;
using Game.Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class TestingToolsView : AbstractPayloadView<TestingToolsViewModel>
    {
        [SerializeField] private Button _makeDamageButton;
        [SerializeField] private Button _healDamageButton;

        [Inject]
        protected override void Construct(TestingToolsViewModel viewModel)
        {
            base.Construct(viewModel);
            _makeDamageButton.onClick.AddListener(_viewModel.MakeDamage);
            _healDamageButton.onClick.AddListener(_viewModel.HealDamage);
        }

        private void OnDestroy()
        {
            _makeDamageButton.onClick.RemoveListener(_viewModel.MakeDamage);
            _healDamageButton.onClick.RemoveListener(_viewModel.HealDamage);
        }
    }
}