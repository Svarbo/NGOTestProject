using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;

public class Interlocutor : NetworkBehaviour
{
    private const string NameDefaultValue = " ";

    //public NetworkVariable<FixedString32Bytes> Name = new NetworkVariable<FixedString32Bytes>(NameDefaultValue,
    //    NetworkVariableReadPermission.Everyone,
    //    NetworkVariableWritePermission.Owner);

    public string Name { get; private set; }

    public event Action<string, string> MessageReady;

    //public override void OnNetworkSpawn()
    //{
    //    Name.OnValueChanged += ChangeInterlocutorName;
    //}

    private void ChangeInterlocutorName(FixedString32Bytes previousValue, FixedString32Bytes currentValue)
    {
    }

    public void SetName(string name)
    {
        Name = name;


        //if (IsOwner)
        //{
        //    Name.Value = name;    
        //    Debug.Log(Name.Value);
        //}
    }

    public void SendMessage(string message)
    {
        string senderName;

        if (Name == NameDefaultValue)
            senderName = "Anonimus";
        else
            senderName = Name.ToString();

        MessageReady?.Invoke("Me", message);
        SendMessageServerRpc(senderName, message, new ServerRpcParams());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendMessageServerRpc(string sourceInterlocutorName, string message, ServerRpcParams serverRpcParams)
    {
        ClientRpcParams clientRpcParams = PrepareClientRpcParams(serverRpcParams);
        ReceiveMessageClientRpc(sourceInterlocutorName, message, clientRpcParams);
    }

    [ClientRpc]
    private void ReceiveMessageClientRpc(string sourceInterlocutorName, string message, ClientRpcParams clientRpcParams) =>
        MessageReady?.Invoke(sourceInterlocutorName, message);

    private ClientRpcParams PrepareClientRpcParams(ServerRpcParams serverRpcParams)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams();
        List<ulong> targetClientsIds = new List<ulong>();

        IReadOnlyList<ulong> connectedClients = NetworkManager.ConnectedClientsIds;
        targetClientsIds.AddRange(connectedClients);

        ulong senderId = serverRpcParams.Receive.SenderClientId;
        targetClientsIds.Remove(senderId);

        clientRpcParams.Send.TargetClientIds = targetClientsIds;

        return clientRpcParams;
    }
}