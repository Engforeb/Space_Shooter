namespace Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Boot = "Boot";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Boot, onLoaded: EnterLoadLevel);
        }
        private void EnterLoadLevel()
        {
            
        }
        private void RegisterServices()
        {
            
        }
        public void Exit()
        {
     
        }
    }
}
