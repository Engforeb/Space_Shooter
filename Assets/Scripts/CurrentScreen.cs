using Infrastructure.Services;
using Interfaces;
using MyScreen;
using UnityEngine;

public class CurrentScreen : IGetSizeable, IService
{
    public float Width { get; }
    public float Height { get;}

    public CurrentScreen(Camera myCamera)
    {
        var camera = myCamera;
        var screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        Width = screenBounds.x;
        Height = screenBounds.y;
    }
    
    public ScreenBounds BoundsForObject(IGetSizeable go)
    {
        var bounds = new ScreenBounds
        {
            Left = -Width + go.Width / 2,
            Right = Width - go.Width / 2,
            Top = Height - go.Height / 2,
            Bottom = -Height + go.Height / 2
        };

        return bounds;
    }
}