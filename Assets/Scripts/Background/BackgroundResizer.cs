using Infrastructure.Services;
using UnityEngine;
namespace Background
{
    public class BackgroundResizer : MonoBehaviour, IResizable
    {
        private IScreenAdjustable _screenAdjustableService;

        public void Resize()
        {
            _screenAdjustableService = AllServices.Container.Single<IScreenAdjustable>();

            float resizeFactor = _screenAdjustableService.ResizeFactor;
            
            Transform mTransform = transform;
            
            Vector3 localScale = mTransform.localScale;
            float xScale = localScale.x;
            float yScale = localScale.y;
            
            localScale = new Vector3(xScale * resizeFactor, yScale * resizeFactor, 1);
            mTransform.localScale = localScale;
        }
    }
}
