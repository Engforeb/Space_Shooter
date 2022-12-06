using Background.Infrastructure.States;
using Infrastructure.Services;
using UnityEngine;
namespace Background
{
    public class BackgroundResizer : MonoBehaviour, IResizable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private IScreenAdjustable _screenAdjustableService;

        public void Resize()
        {
            _screenAdjustableService = AllServices.Container.Single<IScreenAdjustable>();

            float resizeFactor = _screenAdjustableService.ResizeFactor;
            
            var mTransform = transform;
            
            var localScale = mTransform.localScale;
            var xScale = localScale.x;
            var yScale = localScale.y;
            
            localScale = new Vector3(xScale * resizeFactor, yScale * resizeFactor, 1);
            mTransform.localScale = localScale;
        }
    }
}
