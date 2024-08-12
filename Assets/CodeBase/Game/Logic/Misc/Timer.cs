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

    private float _currentTime;

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
        _currentTime = _time;
        _invokeComplete = callback;
        _invokeTick = callTick;
        _step = step;
        await UniTask.WaitForFixedUpdate(_cts.Token);
    }

    public void Play()
    {
        if (_currentTime == 0 ||
            _currentTime != _time)
            return;
        ExecuteAsync();
    }

    public void Pause()
        => _cts.Cancel();

    public void Stop()
    {
        _currentTime = 0;
        _invokeTick?.Invoke(_currentTime);
        _cts.Cancel();
    }

    private async UniTask ExecuteAsync()
    {
        do
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Min(_step, _currentTime)),
                delayTiming: PlayerLoopTiming.FixedUpdate, cancellationToken: _cts.Token);

            if (!_cts.IsCancellationRequested)
            {
                _currentTime -= _step;
                _invokeTick?.Invoke(_currentTime);
            }
        } while (_currentTime > 0f && !_cts.IsCancellationRequested);

        if (_currentTime <= 0 && !_cts.IsCancellationRequested)
            _invokeComplete?.Invoke();
    }

}
