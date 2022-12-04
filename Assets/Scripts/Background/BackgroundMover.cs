using UnityEngine;

namespace Background
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] protected float backgroundSpeed = 0.5f;
        private BackgroundCompositor _backgroundCompositor;

        private SpriteRenderer _spriteRenderer;
        private float _myHeight;
        private GameObject[] _mySiblingsAndI;
        private float _backgroundSizeYOffset;

        private void Awake()
        {
            _backgroundCompositor = FindObjectOfType<BackgroundCompositor>();
            
        }

        private void Start()
        {
            var myTag = gameObject.tag;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _myHeight = _spriteRenderer.bounds.size.y;
            _mySiblingsAndI = GameObject.FindGameObjectsWithTag(myTag);
            _backgroundSizeYOffset = _backgroundCompositor.BackgroundSize(_mySiblingsAndI).y;
        }
        protected void LateUpdate()
        {
            Transform transform1 = transform;
            transform1.position += -transform1.up * (Time.deltaTime * backgroundSpeed);

            if (!(transform.position.y <= -_myHeight)) return;
            var moveUpY = MoveUpY(_mySiblingsAndI);
            transform.position = new Vector2(0, moveUpY-0.01f);
        }

        private float MoveUpY(GameObject[] backgrounds)
        {
            float highestY = -100f;

            for (int i = 0; i < _backgroundCompositor.Quantity; i++)
            {
                if (backgrounds[i].transform.position.y > highestY) 
                    highestY = backgrounds[i].transform.position.y;
            }
            return highestY + _backgroundSizeYOffset;
        }
    }
}
