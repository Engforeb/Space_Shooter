using Ammo;
using Infrastructure.States;
using Logic;
using MyScreen;
using UnityEngine;
namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtain;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BulletContainer bulletParent;

        private Camera _camera;
        private CameraShake _cameraShake;

        private Game _game;

        private void Awake()
        {
            _camera = Camera.main;

            if (_camera != null)
            {
                _cameraShake = _camera.GetComponent<CameraShake>();

                _game = new Game(this, curtain, _camera, spriteRenderer, bulletParent, _cameraShake);
            }
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
