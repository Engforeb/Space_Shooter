using Infrastructure.Services;
using UnityEngine;
public interface IPool : IService
{
    public void Generate();
    public GameObject Add();
    public GameObject Request();
}
