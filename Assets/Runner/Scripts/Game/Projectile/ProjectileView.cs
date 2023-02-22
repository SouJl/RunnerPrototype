using UnityEngine;

namespace Runner.Game
{
    internal class ProjectileView : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private void Awake()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}
