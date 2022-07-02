using System;
using System.Collections.Generic;
namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader)
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            IState state = GetState<TState>();
            _activeState = state;
            state.Enter();
        }
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            IPayloadedState<TPayload> state = GetState<TState>();
            _activeState = state;
            state.Enter(payload);
        }
    }

}
