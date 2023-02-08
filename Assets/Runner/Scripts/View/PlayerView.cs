using System.Collections;
using UnityEngine;

namespace Runner.Scripts.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _runSprites;
        [Range(0 , 1)]
        [SerializeField] private float _animationDelay = 0.1f;

        private bool _playAnimation;
        private int _currentAnim;

        private void Start()
        {
            _currentAnim = 0;
            _playAnimation = true;
            StartCoroutine(PlayAnimation());
        }

        IEnumerator PlayAnimation() 
        {
            while (_playAnimation) 
            {
                _currentAnim = (_currentAnim + 1) % _runSprites.Length;
                _spriteRenderer.sprite = _runSprites[_currentAnim];

                yield return new WaitForSeconds(_animationDelay);
            }           
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            _playAnimation = false;
            StopAllCoroutines();
        }
    }
}
