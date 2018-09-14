using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Webkit;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Nfield.Aad.Droid.Services;
using Nfield.Aad.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]
namespace Nfield.Aad.Droid.Services
{
    public class Authenticator : IAuthenticator
    {
        private AuthenticationContext _authContext;

        public async Task<AuthenticationResult> AuthenticateAsync(string authority, string resource, string clientId, string returnUri)
        {
            try
            {
                Context(authority);

                var uri = new Uri(returnUri);
                var platformParams = new PlatformParameters((Activity)MainApplication.CurrentContext);
                
                return await _authContext.AcquireTokenAsync(resource, clientId, uri, platformParams).ConfigureAwait(false);
            }
            catch (AdalException ex)
            {
                var t = ex;
                throw;
            }
            catch (Exception e)
            {
                var t = e;
                throw;
            }
           
        }

        public void UnAuthenticate(string authority)
        {
            try
            {
                Context(authority);
                _authContext.TokenCache.Clear();

                CookieManager.Instance.RemoveAllCookie();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Context(string authority)
        {
            _authContext = new AuthenticationContext(authority);

            if (_authContext.TokenCache.ReadItems().Any())
                _authContext = new AuthenticationContext(_authContext.TokenCache.ReadItems().First().Authority);
        }
    }
}