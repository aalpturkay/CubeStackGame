using UnityEngine;

namespace BaseState
{
    public abstract class StateMachine : MonoBehaviour
    {
        public IState CurrentState { get; private set; }
        public IState _previousState;

        private bool _inTransition = false;

        public void ChangeState(IState newState)
        {
            // ensure we are ready for a new state
            if (CurrentState == newState || _inTransition)
                return;
            ChangeStateRoutine(newState);
        }

        private void ChangeStateRoutine(IState newState)
        {
            _inTransition = true;
            // begin our exit sequence, to prepare for new state
            if (CurrentState != null)
                CurrentState.Exit();
            // save our current state, in case we want to return to it
            if (_previousState != null)
                _previousState = CurrentState;

            CurrentState = newState;

            // begin our new enter sequence
            if (CurrentState != null)
                CurrentState.Enter();

            _inTransition = false;
        }

        private void Update()
        {
            if (CurrentState != null && !_inTransition)
                CurrentState.Tick();
        }

        private void FixedUpdate()
        {
            if (CurrentState != null && !_inTransition)
                CurrentState.FixedTick();
        }
    }
}