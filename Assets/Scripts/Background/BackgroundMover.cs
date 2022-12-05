using UnityEngine;

namespace Background
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] protected float backgroundSpeed = 0.5f;
        
        private BackgroundCompositor _backgroundCompositor;
        private SpriteRenderer _spriteRenderer;
        private float _myHeight;
        private float _offset;
        private readonly float _gapCrutch = 0.1f;

        public void Init(float backgroundHeight, float offset)
        {
            _myHeight = backgroundHeight;
            _offset = offset;
        }
        
        protected void LateUpdate()
        {
            Transform transform1 = transform;
            transform1.position += -transform1.up * (Time.deltaTime * backgroundSpeed);

            if (!(transform.position.y <= - (_myHeight + _offset))) return;
            
            var moveUpY = _myHeight * 2 - _offset - _gapCrutch;
            transform.position = new Vector2(0, moveUpY);
        }
    }
}
