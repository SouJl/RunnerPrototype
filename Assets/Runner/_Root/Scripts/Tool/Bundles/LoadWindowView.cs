using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Runner.Scripts.Tool.Bundles
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadSpriteAssetsButton;
        [SerializeField] private Button _loadAudioAssetsButton;
        [SerializeField] private Button _changeBackgroundButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private void Start()
        {
            _loadSpriteAssetsButton.onClick.AddListener(LoadSpriteAssets);
            _loadAudioAssetsButton.onClick.AddListener(LoadAudioAssets);
            _changeBackgroundButton.onClick.AddListener(LoadAndChangeBackground);
            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
        }

        private void OnDestroy()
        {
            _loadSpriteAssetsButton.onClick.RemoveListener(LoadSpriteAssets);
            _loadAudioAssetsButton.onClick.RemoveListener(LoadAudioAssets);
            _changeBackgroundButton.onClick.RemoveListener(LoadAndChangeBackground);
            _spawnAssetButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
        }

        private void LoadSpriteAssets()
        {
            _loadSpriteAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetSpritesAssetBundles());
        }

        private void LoadAudioAssets()
        {
            _loadAudioAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAudioAssetBundles());
        }

        private void LoadAndChangeBackground()
        {
            _changeBackgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetBackgroundSpriteBundles());
        }

        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }
    }
}
