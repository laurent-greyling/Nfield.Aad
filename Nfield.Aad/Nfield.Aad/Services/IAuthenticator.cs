using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace Nfield.Aad.Services
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> AuthenticateAsync(string authority, string resource, string clientId, string returnUri);

        void UnAuthenticate(string authority);
    }
}
