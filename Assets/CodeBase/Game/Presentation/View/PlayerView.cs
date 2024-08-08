using Core.MVVM.View;
using DG.Tweening;
using Game.Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class PlayerView : AbstractPayloadView<PlayerViewModel>
    {
        [SerializeField] private Image _reloadBarImage;
        [SerializeField] private Image _hitsBarImage;

        [Inject]
        protected override void Construct(PlayerViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.InvokeReloadActive += ReloadActive;
            _viewModel.InvokeHitsUpdate += HitsUpdate;
        }

        private void ReloadActive(float reloadTime)
        {
            _reloadBarImage.DOKill();
            Debug.Log("reload view");
            _reloadBarImage.gameObject.SetActive(true);
            _reloadBarImage.DOFillAmount(1f, reloadTime).OnKill(
                () => 
                {
                    _reloadBarImage.fillAmount = 0;
                    _reloadBarImage.gameObject.SetActive(false);
                });
        }

        private void HitsUpdate(float ratioHits)
        {
            _hitsBarImage.fillAmount = ratioHits;
        }
    }
}