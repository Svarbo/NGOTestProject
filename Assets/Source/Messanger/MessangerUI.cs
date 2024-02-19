using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessangerUI : MonoBehaviour
{
    [SerializeField] private Interlocutor _interlocutor;
    [SerializeField] private TMP_InputField _messageField;
    [SerializeField] private TMP_InputField _interlocutorNameField;
    [SerializeField] private Button _sendButton;
    [SerializeField] private Button _setNameButton;
    [SerializeField] private GameObject _messagePrefab;
    [SerializeField] private Transform _messagesScrollViewContent;

    private void OnEnable()
    {
        _interlocutor.MessageReady += ShowMessage;
        _sendButton.onClick.AddListener(SendMessage);
        _setNameButton.onClick.AddListener(SetName);
    }

    private void OnDisable()
    {
        _interlocutor.MessageReady += ShowMessage;
        _sendButton.onClick.RemoveListener(SendMessage);
        _setNameButton.onClick.RemoveListener(SetName);
    }

    public void ShowMessage(string interculatorName, string message)
    {
        GameObject instantiatedObject = Instantiate(_messagePrefab, _messagesScrollViewContent);

        if (instantiatedObject.TryGetComponent<MessageView>(out MessageView messageView))
            messageView.Fill(interculatorName, message);
    }

    private void SendMessage()
    {
        _interlocutor.SendMessage(_messageField.text);
        _messageField.text = "";
    }

    private void SetName() =>
        _interlocutor.SetName(_interlocutorNameField.text);
}