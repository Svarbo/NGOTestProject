using System;
using TMPro;
using UnityEngine;

public class MessageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _interculatorNamePlace;
    [SerializeField] private TMP_Text _messagePlace;

    public event Action<string, string> MessageReady;

    public void Fill(string interculatorName, string message)
    {
        _interculatorNamePlace.text = interculatorName;
        _messagePlace.text = message;
    }
}