using Runner.Scripts.Enums;
using UnityEngine;

namespace Runner.Scripts 
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUi;

        private const GameState InirialState = GameState.Start;
       
        void Start()
        {

        }

        private void OnDestroy()
        {
            
        }
    }
}

