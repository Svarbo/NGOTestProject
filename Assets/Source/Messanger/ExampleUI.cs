using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class ExampleUI : MonoBehaviour
{
    [SerializeField] private Button _sendButton;
    [SerializeField] private TMP_InputField _messageField;
    [SerializeField] private Button _nameSetButton;
    [SerializeField] private TMP_InputField _nameField;

    private Interlocutor _interlocutor;

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += SetInterlocutor;

        _nameSetButton.onClick.AddListener(SetName);
        _sendButton.onClick.AddListener(SendMessage);
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= SetInterlocutor;

        _nameSetButton.onClick.RemoveListener(SetName);
        _sendButton.onClick.RemoveListener(SendMessage);
    }

    private void SetName()
    {
        _interlocutor.SetName(_nameField.text);
    }

    private void SendMessage()
    {
        _interlocutor.SendMessage(_messageField.text);
    }

    private void SetInterlocutor(ulong clientId)
    {
        _interlocutor = FindAnyObjectByType<Interlocutor>();
    }
}