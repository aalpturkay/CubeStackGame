using BaseState;

namespace Player.PlayerStates
{
    public class PlayerRunState : IState
    {
        private PlayerSM.PlayerSM _playerSm;

        public PlayerRunState(PlayerSM.PlayerSM playerSm)
        {
            _playerSm = playerSm;
        }

        public void Enter()
        {
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }
    }
}