using Infrastructure.States;
using Logic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtain;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private Camera _camera;
        
        private Game _game;
        private void Awake()
        {
            _camera = Camera.main;
            _game = new Game(this, curtain, _camera, spriteRenderer);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }

}
