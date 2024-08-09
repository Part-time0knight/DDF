using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class Timer
{
    private Action _invokeComplete;
    private Action<float> _invokeTick;

    private CancellationTokenSource _cts;

    private float _time;

    private float _tick;

    private float _step;

    public Timer()
    {
        _cts = new();
        
    }

    public void Initialize(float time, Action callback)
    {
        Initialize(time, 0.5f, null, callback);
    }

    public void Initialize(float time, float step, Action callback)
    {
        Initialize(time, step, null, callback);
    }

    public void Initialize(float time, Action<float> callTick, Action callback)
    {
        Initialize(time, 0.5f, callTick, callback);
    }

    public async void Initialize(float time, float step, Action<float> callTick,  Action callback)
    {
        _time = time;
        _tick = _time;
        _invokeComplete = callback;
        _invokeTick = callTick;
        _step = step;
        await UniTask.WaitForFixedUpdate(_cts.Token);
    }

    public async UniTask Play()
    {
        if (_tick == 0 ||
            _tick != _time)
            return;
        await ExecuteAsync();
    }

    public void Pause()
        => _cts.Cancel();

    public void Stop()
    {
        _tick = 0;
        _invokeTick?.Invoke(_tick);
        _cts.Cancel();
    }

    private async UniTask ExecuteAsync()
    {
        float second;

        

        do
        {
            second = _tick > _step ? _step : _tick;

            await UniTask.Delay(TimeSpan.FromSeconds(second),
                false, PlayerLoopTiming.FixedUpdate, _cts.Token);

            if (!_cts.IsCancellationRequested)
            {
                _tick -= second;
                _invokeTick?.Invoke(_tick);
            }
        } while (_tick > 0f && !_cts.IsCancellationRequested);

        if (_tick == 0 && !_cts.IsCancellationRequested)
            _invokeComplete?.Invoke();
    }

}
