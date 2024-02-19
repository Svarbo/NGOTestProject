using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MessageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _interculatorNamePlace;
    [SerializeField] private TMP_Text _messagePlace;

    private Image _image;

    public event Action<string, string> MessageReady;

    private void Awake() =>
        _image = GetComponent<Image>();

    public void Fill(string interculatorName, string message)
    {
        _interculatorNamePlace.text = interculatorName;
        _messagePlace.text = message;
    }
}