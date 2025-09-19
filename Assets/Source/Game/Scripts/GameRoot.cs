using Authorization;
using ConstantValues;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private AuthorizationWindow _authorizationWindow;

        private Authorizator _authorizator;

        private async void Awake()
        {
            await UnityServices.InitializeAsync();
            PrepareAuthorizationScene();
        }

        private void OnEnable() =>
            SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnDisable() =>
            SceneManager.sceneLoaded -= OnSceneLoaded;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case SceneNames.AutherizationSceneName:
                    //PrepareGameScene();
                    break;
            }
        }

        private void PrepareAuthorizationScene()
        {
            _authorizator = new Authorizator();

            _authorizationWindow.Construct(_authorizator);
        }
    }
}