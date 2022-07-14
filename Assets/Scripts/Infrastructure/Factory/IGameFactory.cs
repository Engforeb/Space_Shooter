using System;
using Infrastructure.Services;
using UnityEngine;
namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        GameObject CreatePlayer(GameObject at);
        void CreateSpawnManager();
        void CreateHud();
    }
}
