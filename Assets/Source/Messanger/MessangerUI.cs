using TMPro;
using Unity.Netcode;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class MessangerUI : MonoBehaviour
{
    [SerializeField] private Interlocutor _interlocutor;
    [SerializeField] private MessagesScrollView _messagesScrollView;
    [SerializeField] private TMP_InputField _messageField;
    [SerializeField] private Button _sendButton;

    private async void OnEnable()
    {
        await UnityServices.InitializeAsync();

        NetworkManager.Singleton.OnClientConnectedCallback += SetInterlocutor;
        _sendButton.onClick.AddListener(SendMessage);
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= SetInterlocutor;
        _interlocutor.MessageReady += _messagesScrollView.ShowMessage;
        _sendButton.onClick.RemoveListener(SendMessage);
    }

    private void SendMessage() =>
        _interlocutor.SendMessage(_messageField.text);

    private void SetInterlocutor(ulong clientId)
    {
        _interlocutor = FindAnyObjectByType<Interlocutor>();
        _interlocutor.MessageReady += _messagesScrollView.ShowMessage;
    }
}