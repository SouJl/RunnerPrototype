using Runner.Scripts.Interfaces;
using Runner.Scripts.Tool;
using Runner.Scripts.View;
using UnityEngine;

namespace Runner.Scripts.Game
{
    internal class PlayerController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Player");

        private readonly PlayerView _view;

        public GameObject ViewGameObject => _view.gameObject;

        public PlayerController() 
        {
            _view = LoadView();
        }

        private PlayerView LoadView() 
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<PlayerView>();
        }
    }
}
