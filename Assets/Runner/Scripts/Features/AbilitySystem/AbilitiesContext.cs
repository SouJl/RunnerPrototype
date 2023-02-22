using Runner.Features.AbilitySystem.Abilities;
using Runner;
using Runner.Interfaces;
using Runner.Tool;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.Features.AbilitySystem
{
    internal class AbilitiesContext : BaseContext<AbilityItemConfig, AbilitiesRepository, AbilitiesView>
    {
        private readonly ResourcePath _dataSourcePath = new("Configs/Ability/AbilityItemsDataConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/AbilitiesView");

        public AbilitiesContext(
            [NotNull] Transform placeForUi,
            [NotNull] IAbilityActivator activator)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (activator == null)
                throw new ArgumentNullException(nameof(activator));

            CreateController(placeForUi, activator);
        }

        private void CreateController(Transform placeForUi, IAbilityActivator activator)
        {
            AbilityItemConfig[] itemConfigs = LoadConfigs();
            AbilitiesRepository repository = CreateRepository(itemConfigs);

            AbilitiesView view = CreateView(placeForUi);
            var controller = new AbilitiesController(view, repository, activator, itemConfigs);

            AddController(controller);
        }

        protected override AbilityItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);


        protected override AbilitiesRepository CreateRepository(AbilityItemConfig[] configsData)
        {
            var repository = new AbilitiesRepository(configsData);
            AddRepository(repository);

            return repository;
        }

        protected override AbilitiesView CreateView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }   
    }
}
