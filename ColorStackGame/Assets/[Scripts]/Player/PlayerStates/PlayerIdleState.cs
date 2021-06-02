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
            _playerSm.SlideHand.SetActive(true);
            Debug.Log("Player Enter State");
        }

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _playerSm.ChangeState(_playerSm.RunState);
            }
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
            _playerSm.SlideHand.SetActive(false);
        }
    }
}