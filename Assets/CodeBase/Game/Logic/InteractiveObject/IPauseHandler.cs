namespace Game.Logic.InteractiveObject
{
    public interface IPauseHandler
    {
        bool Active { get; set; }

        void SubscribeElement(IPauseble pausebleObject);

        void UnsubscribeElement(IPauseble pausebleObject);

    }
}