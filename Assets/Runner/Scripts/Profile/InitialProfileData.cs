using UnityEngine;

namespace Runner.Profile
{

    internal interface IProfileData 
    {
        GameState GameState { get; }
        InputType InputType { get; }
        PlayerData PlayerData { get; }
    }

    [CreateAssetMenu(fileName = nameof(InitialProfileData),
        menuName = "Configs/GameData/" + nameof(InitialProfileData))]
    internal class InitialProfileData : ScriptableObject, IProfileData
    {
        [field: SerializeField] public GameState GameState { get; private set; } = GameState.Start;

        [field: SerializeField] public InputType InputType { get; private set; }

        [field: SerializeField] public PlayerData PlayerData { get; private set; }
    }
}
