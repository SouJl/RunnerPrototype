using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Scripts.Tool.Bundles
{
    [Serializable]
    internal class DataMultiplySpriteBundle
    {
        [field: SerializeField] public string NameRootSpiteAsset { get; private set; }
        [field: SerializeField] public List<DataSpriteBundle> DataSpriteBundles { get; private set; }
    }
}
