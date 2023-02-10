using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsListener
    {
        public event Action Started;
        public event Action Finished;
        public event Action Failed;
        public event Action Skipped;
        public event Action BecomeReady;

        protected readonly string Id;

        protected UnityAdsPlayer(string id) 
        {
            Id = id;
            Advertisement.AddListener(this);
        }

        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void Load();
        protected abstract void OnPlaying();
        
        void IUnityAdsListener.OnUnityAdsReady(string placementId)
        {
            if (IdCompare(placementId) == false) return;

            Log($"Ready {Id}");
            BecomeReady?.Invoke();
        }

        void IUnityAdsListener.OnUnityAdsDidError(string message) => Error($"Error {message}");

        void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
        {
            if (IdCompare(placementId) == false) return;

            Log($"Started {Id}");
            Started?.Invoke();
        }

        void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (IdCompare(placementId) == false) return;

            switch (showResult)
            {
                case ShowResult.Finished: 
                    {
                        Log($"Finished {Id}");
                        Finished?.Invoke();
                        break;
                    }
                case ShowResult.Failed:
                    {
                        Error($"Failed {Id}");
                        Failed?.Invoke();
                        break;
                    }
                case ShowResult.Skipped:
                    {
                        Log($"Skipped {Id}");
                        Skipped?.Invoke();
                        break;
                    }
            }
        }


        private bool IdCompare(string id) => Id == id;

        private void Log(string message) => Debug.Log(WraMessage(message));

        private void Error(string message) => Debug.LogError(WraMessage(message));

        private string WraMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
