using System;
using BaseState;
using Player.PlayerStates;
using UnityEngine;

namespace Player.PlayerSM
{
    public class PlayerSM : StateMachine
    {
        private PlayerIdleState _idleState;
        private PlayerRunState _runState;
        
        public PlayerIdleState IdleState => _idleState;

        public PlayerRunState RunState => _runState;

        private void Awake()
        {
            _idleState = new PlayerIdleState(this);
            _runState = new PlayerRunState(this);
        }

        private void Start()
        {
            ChangeState(_idleState);
        }
    }
}