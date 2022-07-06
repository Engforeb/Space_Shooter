using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
namespace Infrastructure.States
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
        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
        
        private void RegisterServices()
        {
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }
        public void Exit()
        {
     
        }
    }
}
