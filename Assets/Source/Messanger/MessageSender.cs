using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Core;
using UnityEngine;

public class MessageSender : NetworkBehaviour
{
    public NetworkVariable<int> _number = new NetworkVariable<int>(1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private NetworkVariable<List<int>> _loggins = new NetworkVariable<List<int>>();

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        NetworkManager.Singleton.OnClientConnectedCallback += SendHello;
        _number.OnValueChanged += Get;

        _number.Value = 0;
    }

    private void Get(int previousValue, int newValue)
    {
        Debug.Log(newValue + OwnerClientId.ToString());
    }

    private void SendHello(ulong clientId)
    {

    }

    [ServerRpc]
    private void SendHelloToServerRpc()
    {

    }
}

public struct SendStruct : INetworkSerializable
{
    public int Number;
    public bool IsReady;
    public string Name;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Number);
        serializer.SerializeValue(ref IsReady);
        serializer.SerializeValue(ref Name);
    }
}