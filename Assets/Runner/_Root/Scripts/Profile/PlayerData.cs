using UnityEngine;

namespace Runner.Profile
{
    internal interface IPlayerData 
    {
        float Speed { get; }
        float JumpHeight { get; }
        float HealtPoints { get; }

    }

    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = "Configs/GameData/" + nameof(PlayerData))]
    internal class PlayerData : ScriptableObject, IPlayerData
    {
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public float JumpHeight { get; private set; }

        [field: SerializeField] public float HealtPoints { get; private set; }
    }
}
