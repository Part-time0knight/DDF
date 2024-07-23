using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class Timer
{
    public event Action InvokeComplete;

    private Random _random;
    private float _tick;

    private CancellationTokenSource _cts;

    private float _time;

    public Timer()
    {
        _random = new();

    }

    public Timer(float time) : this()
    {
        _time = time;
        _tick = _time;
        _cts = new();
        ExecuteAsync();
    }

    public void Play()
    {
        if (_tick == 0)
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
