﻿using System;
using UnityEngine;

namespace Runner
{
    internal interface IContext : IDisposable 
    { }

    internal abstract class BaseContext<TConfig, TRepository, TView> : LifeCycleObject, IContext
    {
        protected abstract TConfig[] LoadConfigs();

        protected abstract TRepository CreateRepository(TConfig[] configsData);

        protected abstract TView CreateView(Transform placeForUis);
    }
}
