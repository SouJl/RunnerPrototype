using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Scripts.Tool.Bundles
{
    [Serializable]
    internal class DataSpriteBundle
    {
        [field: SerializeField] public string NameAssetBundle { get; private set; }
        [field: SerializeField] public Image Image { get; private set; }
    }
}
