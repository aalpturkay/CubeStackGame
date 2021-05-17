using BaseState;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerIdleState : IState
    {
        private PlayerSM.PlayerSM _playerSm;

        public PlayerIdleState(PlayerSM.PlayerSM playerSm)
        {
            _playerSm = playerSm;
        }

        public void Enter()
        {
            Debug.Log("Player Enter State");
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