using UnityEngine;

namespace Background
{
    public class BackgroundMover : MonoBehaviour, IMoveUppable
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
        
        protected void Update()
        {
            Move();
            if (!(transform.position.y <= - (_myHeight + _offset))) return;
            MoveUp();
        }
        public void MoveUp()
        {
            var moveUpY = _myHeight * 2 - _offset - _gapCrutch;
            transform.position = new Vector2(0, moveUpY);
        }
        public void Move()
        {
            Transform transform1 = transform;
            transform1.position += -transform1.up * (Time.deltaTime * backgroundSpeed);
        }
    }
}
