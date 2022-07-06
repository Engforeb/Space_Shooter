using Logic;
using UnityEngine;


namespace Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }
        
        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        
        public void Exit() => 
            _curtain.Hide();
        
        private void OnLoaded()
        {
            GameObject player = _gameFactory.CreatePlayer();
            _gameFactory.CreateHud();
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
