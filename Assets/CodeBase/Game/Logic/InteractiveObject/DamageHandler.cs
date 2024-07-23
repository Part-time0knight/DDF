
namespace Game.Logic.InteractiveObject
{
    public class DamageHandler
    {
        private ObjectStats _stats;

        public DamageHandler(ObjectStats stats) 
        {
            _stats = stats;
        }

        public void TakeDamage(int damage)
            => _stats.CurrentHits -= damage;
    }
}