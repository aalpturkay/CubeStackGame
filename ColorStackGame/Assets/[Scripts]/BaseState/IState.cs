namespace BaseState
{
    public interface IState
    {
        void Enter();


        void Tick();


        void FixedTick();


        void Exit();
    }
}