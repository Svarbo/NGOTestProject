using System.Collections.Generic;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Internal;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using DeleteOptions = Unity.Services.CloudSave.Models.Data.Player.DeleteOptions;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;

namespace Saving
{
    public class CloudSaver
    {
        private IPlayerDataService _playerDataService = CloudSaveService.Instance.Data.Player;

        public async void SaveData(Dictionary<string, object> savableData)
        {
            await _playerDataService.SaveAsync(savableData);

            Debug.Log($"Saved data {string.Join(',', savableData)}");
        }

        public async void SavePublicData()
        {
            var data = new Dictionary<string, object> { { "keyName", "value" } };

            await _playerDataService.SaveAsync(data, new SaveOptions(new PublicWriteAccessClassOptions()));
        }

        public async void LoadData()
        {
            var playerData = await _playerDataService.LoadAsync(new HashSet<string> { "keyName" });

            if (playerData.TryGetValue("keyName", out var keyName))
                Debug.Log($"keyName: {keyName.Value.GetAs<string>()}");
        }

        public async void LoadPublicData()
        {
            var playerData = await _playerDataService.LoadAsync(new HashSet<string> { "keyName" }, new LoadOptions(new PublicReadAccessClassOptions()));

            if (playerData.TryGetValue("keyName", out var keyName))
                Debug.Log($"keyName: {keyName.Value.GetAs<string>()}");
        }

        public async void LoadPublicDataByPlayerId(string playerId)
        {
            var playerData = await _playerDataService.LoadAsync(new HashSet<string> { "keyName" }, new LoadOptions(new PublicReadAccessClassOptions(playerId)));

            if (playerData.TryGetValue("keyName", out var keyName))
                Debug.Log($"keyName: {keyName.Value.GetAs<string>()}");
        }

        public async void DeleteData(string key)
        {
            DeleteOptions options = new DeleteOptions();

            await _playerDataService.DeleteAsync(key, options);
        }

        public async void LogListKeys()
        {
            var keys = await _playerDataService.ListAllKeysAsync();

            for (int i = 0; i < keys.Count; i++)
                Debug.Log(keys[i].Key);
        }
    }
}