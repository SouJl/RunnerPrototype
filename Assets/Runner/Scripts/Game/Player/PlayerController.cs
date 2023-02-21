using Runner.Interfaces;
using Runner.Tool;
using Runner.View;
using UnityEngine;

namespace Runner.Game
{
    internal class PlayerController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Player");
        
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public GameObject ViewGameObject => _view.gameObject;
        public float JumpHeight => _model.JumpHeight;
        public IPhysicsUnit PhysicsUnit => _view;

        public PlayerController(PlayerModel model) 
        {
            _model = model;
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
