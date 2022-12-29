using UnityEngine;
using Screen = UnityEngine.Device.Screen;
namespace CameraClasses
{
    public class CameraResizer : MonoBehaviour
    {
        [SerializeField] private Camera myCamera;
        private float _bgrWidth;

        private void Init(float bgrWidth)
        {
            _bgrWidth = bgrWidth;
            float size = _bgrWidth * Screen.height / Screen.width * 0.5f;
            myCamera.orthographicSize = size;
        }
    }
}
