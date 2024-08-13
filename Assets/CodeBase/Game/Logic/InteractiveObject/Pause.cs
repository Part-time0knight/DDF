using Game.Infrastructure.Signals;
using Zenject;

namespace Game.Logic.InteractiveObject
{
    public class Pause
    {

        private readonly SignalBus _signalBus;

        private bool _isPaused;

        public Pause(SignalBus signalBus)
        {
            _isPaused = false;
            _signalBus = signalBus;
        }

        public bool Get()
            => _isPaused;

        public void Set(bool pause)
        {
            _isPaused = pause;
            _signalBus.Fire(new PauseSignal(pause));
        }
    }
}