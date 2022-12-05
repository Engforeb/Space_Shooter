using UnityEngine;
namespace Background
{
    public class BackgroundResizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Init(float resizeFactor) => 
            BackgroundResize(resizeFactor);

        private void BackgroundResize(float resizeFactor)
        {
            var mTransform = transform;
            
            var localScale = mTransform.localScale;
            var xScale = localScale.x;
            var yScale = localScale.y;
            
            localScale = new Vector3(xScale * resizeFactor, yScale * resizeFactor, 1);
            mTransform.localScale = localScale;
        }
    }
}
