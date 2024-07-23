using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class Timer
{
    private Action InvokeComplete;

    private float _tick;

    private CancellationTokenSource _cts;

    private float _time;

    public Timer()
    {
        _cts = new();
    }

    public void Initialize(float time, Action callback)
    {
        _time = time;
        _tick = _time;
        InvokeComplete = callback;
    }

    public void Play()
    {
        if (_tick == 0 ||
            _tick != _time)
            return;
        ExecuteAsync();
    }

    public void Pause()
        => _cts.Cancel();

    public void Stop()
    {
        _tick = 0;
        _cts.Cancel();
    }

    private async UniTask ExecuteAsync()
    {
        float second;
        do
        {
            second = _tick > 0.5f ? 0.5f : _tick;

            await UniTask.Delay(TimeSpan.FromSeconds(second),
                false, PlayerLoopTiming.FixedUpdate, _cts.Token);

            if (!_cts.IsCancellationRequested)
                _tick -= second;

        } while (_tick > 0f && !_cts.IsCancellationRequested);

        if (_tick == 0 && !_cts.IsCancellationRequested)
            InvokeComplete?.Invoke();
    }

}
