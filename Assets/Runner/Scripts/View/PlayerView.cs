using Runner.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Runner.Scripts.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal class PlayerView : MonoBehaviour, IPhysicsUnit
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _runSprites;
        [Range(0 , 1)]
        [SerializeField] private float _animationDelay = 0.1f;
        
        [Space(10)]
        [Header("Physics")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayerMask;


        private bool _playAnimation;
        private int _currentAnim;

        public Rigidbody2D UnitRigidBody => _rigidbody;

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
            _rigidbody ??= GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            _playAnimation = false;
            StopAllCoroutines();
        }

        public bool IsGround()
        {
            var hit = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayerMask);
            
            return hit != null;
        }
    }
}
