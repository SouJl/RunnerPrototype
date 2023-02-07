using Runner.Scripts.Profile;
using UnityEngine;

namespace Runner.Scripts
{
    internal class MainContoller : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;


        public MainContoller(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
        }

    }
}
