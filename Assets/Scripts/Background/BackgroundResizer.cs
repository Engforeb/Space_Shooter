using Infrastructure.Services;
using UnityEngine;
namespace Background
{
    public class BackgroundResizer : MonoBehaviour, IResizable
    {
        private IBackgroundAdjuster _backgroundAdjusterService;

        public void Resize()
        {
            _backgroundAdjusterService = AllServices.Container.Single<IBackgroundAdjuster>();

            float resizeFactor = _backgroundAdjusterService.ResizeFactor;

            Transform mTransform = transform;

            Vector3 localScale = mTransform.localScale;
            float xScale = localScale.x;
            float yScale = localScale.y;

            localScale = new Vector3(xScale * resizeFactor, yScale * resizeFactor, 1);
            mTransform.localScale = localScale;
        }
    }
}
