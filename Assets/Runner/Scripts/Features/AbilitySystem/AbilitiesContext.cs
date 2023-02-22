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
    internal class AbilitiesContext: BaseContext
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

        private AbilitiesView CreateView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }

        private AbilityItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);

        private AbilitiesRepository CreateRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }
    }
}
