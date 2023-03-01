using UnityEngine;

namespace Runner.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal class BackgroundView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _relativeSpeedRate;

        private Vector2 _size;
        private Vector2 _cachedPoisition;

        private float LeftBorder => _cachedPoisition.x - _size.x / 2;
        private float RightBorder => _cachedPoisition.x + _size.x / 2;


        private void Awake()
        {
            _cachedPoisition = transform.position;
            _size = _spriteRenderer.size;
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        public void Move(float value)
        {
            Vector3 position = transform.position;
            position += Vector3.right * value * _relativeSpeedRate;

            if (position.x <= LeftBorder) 
            {
                position.x = RightBorder - (LeftBorder - position.x);
            }
            
            if (position.x >= RightBorder) 
            {
                position.x = LeftBorder + (RightBorder - position.x);
            }

            transform.position = position;
        }
    }
}
