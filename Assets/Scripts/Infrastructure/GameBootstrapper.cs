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
        [SerializeField] private int bulletPoolCapacity;
        
        
        private Camera _camera;
        
        private Game _game;
        private void Awake()
        {
            _camera = Camera.main;
            _game = new Game(this, curtain, _camera, spriteRenderer, bulletParent, bulletPoolCapacity);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }

}
