using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Nfield.Aad.Services;
using Nfield.Aad.UWP.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]
namespace Nfield.Aad.UWP.Services
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
                var platformParams = new PlatformParameters(PromptBehavior.Auto, false);
                return await _authContext.AcquireTokenAsync(resource, clientId, uri, platformParams).ConfigureAwait(false);
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

                Windows.Web.Http.Filters.HttpBaseProtocolFilter myFilter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
                var cookieManager = myFilter.CookieManager;
                HttpCookieCollection myCookieJar = cookieManager.GetCookies(new System.Uri("https://login.windows.net/niponfieldusers.onmicrosoft.com"));
                foreach (HttpCookie cookie in myCookieJar)
                {
                    cookieManager.DeleteCookie(cookie);
                }
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
