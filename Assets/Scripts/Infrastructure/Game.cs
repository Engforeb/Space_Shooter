using Infrastructure.Services;
using Infrastructure.States;
using Logic;
using UnityEngine;
namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, Camera camera, SpriteRenderer spriteRenderer)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container, camera, spriteRenderer);
        }
    }
}
