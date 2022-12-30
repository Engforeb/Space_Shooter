using Infrastructure.Services;
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

        private IScreenAdjustable _screenAdjuster;

        public void Init()
        {
            _screenAdjuster = AllServices.Container.Single<IScreenAdjustable>();

            _myHeight = _screenAdjuster.BackgroundsHeight;
            _offset = _screenAdjuster.VerticalOffset;
        }

        protected void Update()
        {
            Move();
            if (!(transform.position.y <= -(_myHeight + _offset)))
            {
                return;
            }
            MoveUp();
        }
        public void MoveUp()
        {
            float moveUpY = _myHeight * 2 - _offset - _gapCrutch;
            transform.position = new Vector2(0, moveUpY);
        }
        public void Move()
        {
            Transform myTransform = transform;
            myTransform.position += -myTransform.up * (Time.deltaTime * backgroundSpeed);
        }
    }
}
