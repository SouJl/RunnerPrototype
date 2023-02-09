using Runner.Scripts.Enums;
using Runner.Scripts.Profile;
using Runner.Scripts.Tool.Analytics;
using UnityEngine;

namespace Runner.Scripts 
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InputType _inputType;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private AnalyticsManager _analytics;

        private const GameState InitialState = GameState.Start;
        
        private MainContoller _mainContoller;

        void Start()
        {
            var profilePlayer = new ProfilePlayer(_inputType, _playerSpeed, InitialState);
            _mainContoller = new MainContoller(_placeForUi, profilePlayer);
        }

        private void OnDestroy()
        {
            _mainContoller.Dispose();
        }
    }
}

