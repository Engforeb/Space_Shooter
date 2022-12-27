using Ammo;
using Background.Infrastructure.States;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Boot = "Boot";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private Camera _camera;
        private SpriteRenderer _spriteRenderer;
        private Transform _bulletParent;
        private int _bulletPoolCapacity;
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services, 
            Camera camera, SpriteRenderer spriteRenderer, Transform bulletParent, int bulletPoolCapacity)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _camera = camera;
            _spriteRenderer = spriteRenderer;
            _bulletParent = bulletParent;
            _bulletPoolCapacity = bulletPoolCapacity;

            RegisterServices();
        }
        public void Enter()
        {
            _sceneLoader.Load(Boot, onLoaded: EnterLoadLevel);
        }
        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadProgressState>();
        
        private void RegisterServices()
        {
            _services.RegisterSingle<IScreenAdjustable>(new ScreenAdjustable(_camera, _spriteRenderer));
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
            _services.RegisterSingle<ISavedLoadService>(new SavedLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
            _services.RegisterSingle<IPool>(new BulletPool(_services.Single<IGameFactory>(), _bulletParent, _bulletPoolCapacity));
        }
        public void Exit()
        {
     
        }
    }
}
