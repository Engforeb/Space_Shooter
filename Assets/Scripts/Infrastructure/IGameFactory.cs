using UnityEngine;
namespace Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreatePlayer();
        GameObject CreatePlayer(GameObject at);
        void CreateHud();
    }
}
