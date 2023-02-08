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
        
        private MainContoller _mainContoller;

        void Start()
        {
            var profilePlayer = new ProfilePlayer(_playerSpeed, InitialState);
            _mainContoller = new MainContoller(_placeForUi, profilePlayer);
        }

        private void OnDestroy()
        {
            _mainContoller.Dispose();
        }
    }
}

