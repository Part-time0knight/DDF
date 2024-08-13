namespace Game.Infrastructure.Signals
{
    public class PauseSignal
    {
        public bool IsPause { get; set; }

        public PauseSignal(bool active)
        {
            IsPause = active;
        }
    }
}