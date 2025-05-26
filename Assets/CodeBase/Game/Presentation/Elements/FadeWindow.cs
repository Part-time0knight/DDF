using DG.Tweening;
using Game.Logic.Enemy.Fsm.States;
using System;
using UnityEngine;

public class FadeWindow : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private Tween _tween;


    public void FadeOn(Action callback)
    {
        Clear();
        _tween = _settings.Group
            .DOFade(1f, _settings.FadeDuration)
            .OnComplete(callback.Invoke);
    }

    public void FadeOff(Action callback)
    {
        Clear();
        _tween = _settings.Group
            .DOFade(0f, _settings.FadeDuration)
            .OnComplete(callback.Invoke);
    }

    private void Clear()
    {
        if (_tween == null) return;
        _tween.Kill();
        _tween = null;
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public float FadeDuration { get; private set; }
        [field: SerializeField] public CanvasGroup Group { get; private set; }
    }
}
