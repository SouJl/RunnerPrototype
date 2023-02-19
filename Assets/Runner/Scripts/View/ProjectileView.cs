using UnityEngine;

namespace Runner.Scripts.View
{
    internal class ProjectileView:MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private void Awake()
        {
            Destroy(gameObject, _lifeTime);
        }        
    }
}
