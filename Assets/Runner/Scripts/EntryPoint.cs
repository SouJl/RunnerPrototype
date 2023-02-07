using Runner.Scripts.Enums;
using Runner.Scripts.Profile;
using UnityEngine;

namespace Runner.Scripts 
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed;
        [SerializeField] private Transform _placeForUi;

        private const GameState InitialState = GameState.Start;
       
        void Start()
        {
            var profilePlayer = new ProfilePlayer(_playerSpeed, InitialState);
        }

        private void OnDestroy()
        {
            
        }
    }
}

