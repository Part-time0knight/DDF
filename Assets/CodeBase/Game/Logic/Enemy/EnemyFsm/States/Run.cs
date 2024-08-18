using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Enemy.EnemyFsm.States
{
    public class Run : IState
    {
        private readonly EnemyMoveHandler _moveHandler;
        private readonly EnemyTickHandler _tickHandler;

        public Run(EnemyMoveHandler moveHandler, EnemyTickHandler tickHandler)
        {
            _moveHandler = moveHandler;
            _tickHandler = tickHandler;
        }

        public void OnEnter()
        {
            _tickHandler.OnFixedTick += UpdateMove;
        }

        public void OnExit()
        {
            _tickHandler.OnFixedTick -= UpdateMove;
        }

        private void UpdateMove()
        {
            _moveHandler.MoveToPlayer();
        }
    }
}