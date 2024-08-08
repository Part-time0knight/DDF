using Core.MVVM.View;
using Game.Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class PlayerReloadView : AbstractPayloadView<PlayerReloadViewModel>
    {
        [SerializeField] private Image _imageBar;

        [Inject]
        protected override void Construct(PlayerReloadViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.InvokeUpdate += ValueUpdate;
            _viewModel.InvokeActive += ActiveUpdate;
        }

        private void ActiveUpdate(bool active)
        {
            _imageBar.gameObject.SetActive(active);
        }

        private void ValueUpdate(float value)
        {
            _imageBar.fillAmount = value;
        }
    }
}