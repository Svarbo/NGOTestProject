using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private PlayerData _playerData = new PlayerData();
    public FixedString64Bytes Name { get; private set; }

    public void SetName(FixedString64Bytes name)
    {
        _playerData.PlayerName = name;
    }
}