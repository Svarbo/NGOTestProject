using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Room : NetworkBehaviour
{
    private NetworkList<PlayerData> _playerDataNetworkList = new NetworkList<PlayerData>();

    private async void OnEnable()
    {
        await UnityServices.InitializeAsync();

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    private void OnClientConnected(ulong id)
    {
        FixedString64Bytes name = "mitch";
        SetPlayerDataServerRpc(AuthenticationService.Instance.PlayerId, name);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerDataServerRpc(string playerId, FixedString64Bytes playerName, ServerRpcParams serverRpcParams = default)
    {
        PlayerData playerData;

        int playerDataIndex = GetPlayerDataIndexFromClientId(serverRpcParams.Receive.SenderClientId);

        if (playerDataIndex < 0)
        {
            playerData = new PlayerData();

            playerData.PlayerName = playerName;
            playerData.PlayerId = playerId;

            _playerDataNetworkList.Add(playerData);
        }
        else
        {
            playerData = _playerDataNetworkList[playerDataIndex];

            playerData.PlayerName = playerName;
            playerData.PlayerId = playerId;

            _playerDataNetworkList[playerDataIndex] = playerData;
        }

        //Debug.Log("Player inited: " + playerName + " Id: " + playerId);
        Debug.Log("Player inited: " + playerData.PlayerName + " Id: " + playerData.PlayerId);
    }

    private int GetPlayerDataIndexFromClientId(ulong clientId)
    {
        for (int i = 0; i < _playerDataNetworkList.Count; i++)
            if (_playerDataNetworkList[i].ClientId == clientId)
                return i;

        return -1;
    }
}