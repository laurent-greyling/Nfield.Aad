using Nfield.Aad.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nfield.Aad.Services
{
    public class RestCall : IRestCall
    {
        readonly HttpClient _httpClient;

        public RestCall()
        {
            _httpClient = new HttpClient();

        }

        public async Task<string> GetAsync(string request, AuthModel auth)
        {
            try
            {
                var message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(request)
                };
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token);
                message.Headers.Add("X-Nfield-Domain", auth.Domain);

                var response = await _httpClient.SendAsync(message).ConfigureAwait(false);

                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var t = ex;
                throw;
            }
            
        }

        public async Task<string> PostAsync(string request, string serialisedData)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(serialisedData, Encoding.Unicode, "application/json");
                var response = await _httpClient.PostAsync(request, content).ConfigureAwait(false);

                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        public async Task<string> PostAsync(string request, AuthModel auth, string serialisedData)
        {
            var content = new StringContent(serialisedData);
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("X-Nfield-Domain", auth.Domain);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token);
            var response = await _httpClient.PostAsync(request, content).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return result;
        }
    }
}
