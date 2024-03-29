﻿using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Runner.Scripts.Tool.Bundles
{
    internal class AssetBundleViewBase : MonoBehaviour
    {
        private const string DownloadSourcePath = "https://drive.google.com/uc?export=download&id=";

        [Header("Id Keys Settings")]
        [SerializeField] private string singleBundleSpritesId;
        [SerializeField] private string multiplyBundleSpritesId;
        [SerializeField] private string audioBundleSpritesId;
        [SerializeField] private string backgroundSpritesId;

        [Header("Sprites")]
        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;
        [SerializeField] private DataMultiplySpriteBundle[] _dataMultiplySpriteBundle;
        [SerializeField] private DataSpriteBundle[] _dataBackgroundSpritesBundles;

        [Space(10)]
        [Header("Audio")]
        [SerializeField] private DataAudioBundle[] _dataAudioBundles;


        private AssetBundle _spritesAssetBundle;
        private AssetBundle _multiplySpritesAssetBundle;
        private AssetBundle _audioAssetBundle;
        private AssetBundle _backgroundSpriteAssetBundle;


        private string _urlAssetBundleSprites;
        private string _urlAssetBundleMultiplySprites;
        private string _urlAssetBundleAudio;
        private string _urlAssetBundleBackgroundSprites;

        private void Awake()
        {
            _urlAssetBundleSprites = DownloadSourcePath + singleBundleSpritesId;
            _urlAssetBundleMultiplySprites = DownloadSourcePath + multiplyBundleSpritesId;
            _urlAssetBundleAudio = DownloadSourcePath + audioBundleSpritesId;
            _urlAssetBundleBackgroundSprites = DownloadSourcePath + backgroundSpritesId;
        }

        protected IEnumerator DownloadAndSetSpritesAssetBundles()
        {
            yield return GetSpritesAssetBundle();
            yield return GetMultiplySpiteAssetBundle();


            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");

            if (_multiplySpritesAssetBundle != null)
                SetMultiplySpriteAssets(_multiplySpritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_multiplySpritesAssetBundle)} failed to load");
        }

        protected IEnumerator DownloadAndSetAudioAssetBundles()
        {
            yield return GetAudioAssetBundle();


            if (_audioAssetBundle != null)
                SetAudioAssets(_audioAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_audioAssetBundle)} failed to load");
        }

        protected IEnumerator DownloadAndSetBackgroundSpriteBundles()
        {
            yield return GetBackgroundBundle();

            if (_backgroundSpriteAssetBundle != null)
                SetBackgroundSpiteAssets(_backgroundSpriteAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_backgroundSpriteAssetBundle)} failed to load");
        }


        private IEnumerator GetSpritesAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleSprites);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _spritesAssetBundle);
        }


        private IEnumerator GetMultiplySpiteAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleMultiplySprites);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _multiplySpritesAssetBundle);
        }

        private IEnumerator GetBackgroundBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleBackgroundSprites);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _backgroundSpriteAssetBundle);
        }

        private IEnumerator GetAudioAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleAudio);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _audioAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log($"{assetBundle}  - Complete!");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetSpriteAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _dataSpriteBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private void SetMultiplySpriteAssets(AssetBundle assetBundle)
        {
            foreach(DataMultiplySpriteBundle dataMultySprite in _dataMultiplySpriteBundle)
            {
                var assetSprites = assetBundle.LoadAssetWithSubAssets<Sprite>(dataMultySprite.NameRootSpiteAsset);
                foreach (DataSpriteBundle data in dataMultySprite.DataSpriteBundles) 
                {
                    data.Image.sprite = assetSprites.Where(s => s.name == data.NameAssetBundle).FirstOrDefault();
                }
            }
        }

        private void SetAudioAssets(AssetBundle assetBundle)
        {
            foreach (DataAudioBundle data in _dataAudioBundles)
            {
                data.AudioSource.clip = assetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
                data.AudioSource.Play();
            }
        }

        private void SetBackgroundSpiteAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _dataBackgroundSpritesBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }
    }
}
