using Nfield.Aad.Models;
using System.Threading.Tasks;

namespace Nfield.Aad.Services
{
    public interface IRestCall
    {
        Task<string> GetAsync(string request, AuthModel auth);
        Task<string> PostAsync(string request, string serialisedData);

        Task<string> PostAsync(string request, AuthModel auth, string serialisedData);
    }
}
