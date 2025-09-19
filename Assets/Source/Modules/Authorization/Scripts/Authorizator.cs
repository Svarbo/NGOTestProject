using ConstantValues;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

namespace Authorization
{
    public class Authorizator
    {
        private IAuthenticationService _authenticationService;

        public Authorizator()
        {
            _authenticationService = AuthenticationService.Instance;
            SetupEvents();
        }

        public async Task SignInAnonymous() =>
            await _authenticationService.SignInAnonymouslyAsync();

        public async Task SignInWithSteam()
        {
            string identity = "Ќе знаю, что это такое, но без него ругаетс€";

            await _authenticationService.SignInWithSteamAsync(AuthenticationTokens.Steam, identity);
        }

        private void SetupEvents()
        {
            _authenticationService.SignedIn += () => {
                Debug.Log("Sign in success!");
                Debug.Log($"PlayerID: {_authenticationService.PlayerId}");
                Debug.Log($"Access Token: {_authenticationService.AccessToken}");
            };

            _authenticationService.SignInFailed += (error) => {
                Debug.LogError(error);
            };

            _authenticationService.SignedOut += () => {
                Debug.Log("Player signed out.");
            };

            _authenticationService.Expired += () =>
            {
                Debug.Log("Player session could not be refreshed and expired.");
            };
        }
    }
}