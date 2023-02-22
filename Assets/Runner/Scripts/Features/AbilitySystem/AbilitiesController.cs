using Runner.Features.AbilitySystem.Abilities;
using Runner.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Runner.Features.AbilitySystem
{
    internal interface IAbilitiesController { }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;


        public AbilitiesController( 
            [NotNull] IAbilitiesView view, 
            [NotNull] IAbilitiesRepository repository,
            [NotNull] IAbilityActivator abilityActivator,
            [NotNull] IEnumerable<IAbilityItem> items)
        {
            _view 
                = view ?? throw new ArgumentNullException(nameof(view));

            _repository 
                = repository ?? throw new ArgumentNullException(nameof(repository));

            _abilityActivator
                = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            _view.Display(items, OnAbilityViewClicked);
        }

        protected override void OnDispose() =>
                _view.Clear();

        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}
