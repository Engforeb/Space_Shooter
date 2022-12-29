using Background.Infrastructure.States;
using UnityEngine;
namespace Infrastructure.States
{
    public class ScreenAdjustable : IScreenAdjustable
    {
        public float BackgroundsHeight { get; private set; }
        public float BackgroundsWidth { get; private set; }
        public float VerticalOffset { get; private set;}
        public float ResizeFactor { get; private set;}
        
        private readonly Camera _camera;
        private readonly SpriteRenderer _spriteRenderer;

        public ScreenAdjustable(Camera camera, SpriteRenderer spriteRenderer)
        {
            _camera = camera;
            _spriteRenderer = spriteRenderer;
            ScreenAdjustmentData();
        }

        private void ScreenAdjustmentData()
        {
            float screenHeight = _camera.orthographicSize * 2;
            float screenWidth = screenHeight / Screen.height * Screen.width;
        
            Sprite sprite = _spriteRenderer.sprite;
            
            ResizeFactor = screenWidth / sprite.bounds.size.x;
            BackgroundsHeight = _spriteRenderer.bounds.size.y;// * ResizeFactor;
            BackgroundsWidth = _spriteRenderer.bounds.size.x;
            VerticalOffset = (screenHeight - BackgroundsHeight) * 0.5f;
            
            float camSize = BackgroundsWidth * Screen.height / Screen.width * 0.5f;
            _camera.orthographicSize = camSize;
        }
    }
}
