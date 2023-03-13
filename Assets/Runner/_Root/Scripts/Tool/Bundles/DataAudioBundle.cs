using System;
using UnityEngine;

namespace Runner.Scripts.Tool.Bundles
{
    [Serializable]
    internal class DataAudioBundle
    {
        [field: SerializeField] public string NameAssetBundle { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
    }
}
