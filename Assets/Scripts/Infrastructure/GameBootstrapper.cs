using Infrastructure.States;
using Logic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtain;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform bulletParent;

        private Camera _camera;
        private int _bulletContainerCapacity;
        
        private Game _game;
        private void Awake()
        {
            _camera = Camera.main;
            _bulletContainerCapacity = bulletParent.GetComponent<BulletContainer>().Capacity;
            _game = new Game(this, curtain, _camera, spriteRenderer, bulletParent, _bulletContainerCapacity);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }

}
