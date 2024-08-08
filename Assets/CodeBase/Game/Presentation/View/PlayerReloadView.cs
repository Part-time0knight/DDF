using Core.MVVM.View;
using DG.Tweening;
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
            _viewModel.InvokeActive += ActiveUpdate;
        }

        private void ActiveUpdate(float reloadTime)
        {
            _imageBar.DOKill();
            Debug.Log("reload view");
            _imageBar.gameObject.SetActive(true);
            _imageBar.DOFillAmount(1f, reloadTime).OnKill(
                () => 
                {
                    _imageBar.fillAmount = 0;
                    _imageBar.gameObject.SetActive(false);
                });
        }
    }
}