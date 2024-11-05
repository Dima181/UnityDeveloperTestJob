using Zenject;
using Infrastructure.View;
using Infrastructure.Scenes;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrap : IInitializable
    {
        [Inject] private readonly BootstrapView _view;
        [Inject] private readonly SceneLoader _sceneLoader;

        public async void Initialize()
        {
            void ApplyProgress(float progress) =>
                _view.SetProgress(progress);

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            GameObject.DontDestroyOnLoad(_view.gameObject);
            ApplyProgress(0);

            await _sceneLoader.LoadGameplay(progressCallback: ApplyProgress, sceneActivationDelay: 2);

            ApplyProgress(1);
            _view.gameObject.SetActive(false);
        }
    }
}