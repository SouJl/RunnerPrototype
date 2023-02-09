using UnityEngine;

namespace Runner.Scripts.Tool
{
    internal class DontDestroyOnLoadObject:MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
                DontDestroyOnLoad(gameObject);
        }
    }
}
