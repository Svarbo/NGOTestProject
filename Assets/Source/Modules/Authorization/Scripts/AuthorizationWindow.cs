using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Authorization
{
    public class AuthorizationWindow : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _loginInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signUpButton;

        private Authorizator _authorizator;

        private void OnDisable()
        {
            if( _authorizator != null)
            {

            }
        }

        public void Construct(Authorizator authorizator)
        {
            _authorizator = authorizator;
            SubscribeUIElements();
        }

        private void SubscribeUIElements()
        {
            _signInButton.onClick.AddListener(OnSignInButtonClick);
            _signUpButton.onClick.AddListener(OnSignUpButtonClick);
        }

        private async void OnSignInButtonClick()
        {
            await _authorizator.SignInAnonymous();
        }

        private void OnSignUpButtonClick()
        {

        }
    }
}