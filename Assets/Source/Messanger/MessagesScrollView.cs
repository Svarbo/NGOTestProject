using UnityEngine;

public class MessagesScrollView : MonoBehaviour
{
    [SerializeField] private GameObject _messagePrefab;
    [SerializeField] private Transform _content;

    public void ShowMessage(string interculatorName, string message)
    {
        GameObject instantiatedObject = Instantiate(_messagePrefab, _content);

        if (instantiatedObject.TryGetComponent<MessageView>(out MessageView messageView))
            messageView.Fill(interculatorName, message);
    }
}