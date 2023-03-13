using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System;

namespace Runner.Scripts.Tool.Bundles
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadSpriteAssetsButton;
        [SerializeField] private Button _loadAudioAssetsButton;
        [SerializeField] private Button _changeBackgroundButton;
        
        [Space(10)]
        [Header("Addressables Spawn Buttons")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;
        
        [Space(10)]
        [Header("Addressables Background")]
        [SerializeField] private AssetReference _backgroundImage;
        [SerializeField] private Image _backgroundImagePlacement;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;


        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private AsyncOperationHandle<Sprite> _loadedBackgroundSprite;

        private void Start()
        {
            SubsribeButtons();
        }

        private void OnDestroy()
        {
            UnsubscribeButtons();
            DespawnPrefabs();
        }
        private void SubsribeButtons()
        {
            _loadSpriteAssetsButton.onClick.AddListener(LoadSpriteAssets);
            _loadAudioAssetsButton.onClick.AddListener(LoadAudioAssets);
            _changeBackgroundButton.onClick.AddListener(LoadAndChangeBackground);
            _spawnAssetButton.onClick.AddListener(SpawnPrefab);

            _addBackgroundButton.onClick.AddListener(AddBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
        }


        private void UnsubscribeButtons()
        {
            _loadSpriteAssetsButton.onClick.RemoveListener(LoadSpriteAssets);
            _loadAudioAssetsButton.onClick.RemoveListener(LoadAudioAssets);
            _changeBackgroundButton.onClick.RemoveListener(LoadAndChangeBackground);
            _spawnAssetButton.onClick.RemoveAllListeners();

            _addBackgroundButton.onClick.RemoveListener(AddBackground);
            _removeBackgroundButton.onClick.RemoveListener(RemoveBackground);
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

        private void AddBackground()
        {
            if (!_loadedBackgroundSprite.IsValid())
            {
                _loadedBackgroundSprite = Addressables.LoadAssetAsync<Sprite>(_backgroundImage);
                _loadedBackgroundSprite.Completed += OnBackgroundLoaded;
            }
        }

        private void RemoveBackground()
        {
            if (_loadedBackgroundSprite.IsValid())
            {
                _loadedBackgroundSprite.Completed -= OnBackgroundLoaded;
                Addressables.Release(_loadedBackgroundSprite);
                SetBackgroundSprite(null);
            }
        }

        private void OnBackgroundLoaded(AsyncOperationHandle<Sprite> asyncOperationHandle)
        {
            asyncOperationHandle.Completed -= OnBackgroundLoaded;
            SetBackgroundSprite(asyncOperationHandle.Result);
        }

        private void SetBackgroundSprite(Sprite sprite) =>
            _backgroundImagePlacement.sprite = sprite;
    }

}
