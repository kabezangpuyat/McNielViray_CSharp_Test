using MNV.Core.Enums;
using MNV.Core.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MNV.Core.Services
{
    public class ApiService : IApiService
    {
        public ApiService()
        {
        }
        public async Task<string> Get(string apiUrl, ApiServiceAuthType authType, string token = null)
        {
            string rawResponse = string.Empty;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (HttpClient client = this.Client(apiUrl, authType, token, httpClientHandler))
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            rawResponse = readTask.GetAwaiter().GetResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return rawResponse;
        }

        #region Post
        public async Task<string> Post<T>(string apiUrl, T model, ApiServiceAuthType authType, string token = null)
        {
            string rawResponse = string.Empty;
            var modelToString = JsonConvert.SerializeObject(model);
            var payload = new StringContent(modelToString, Encoding.UTF8, "application/json");
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (HttpClient client = this.Client(apiUrl, authType, token, httpClientHandler))
                    {
                        HttpResponseMessage response = await client.PostAsync(apiUrl, payload);

                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            rawResponse = readTask.GetAwaiter().GetResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return rawResponse;
        }

        public async Task<string> Post(string apiUrl, string model, ApiServiceAuthType authType, string token = null)
        {
            string rawResponse = string.Empty;
            var payload = new StringContent(model, Encoding.UTF8, "application/json");
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (HttpClient client = this.Client(apiUrl, authType, token, httpClientHandler))
                    {
                        HttpResponseMessage response = await client.PostAsync(apiUrl, payload);

                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            rawResponse = readTask.GetAwaiter().GetResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return rawResponse;
        }
        #endregion

        public async Task<string> Put<T>(string apiUrl, T model, ApiServiceAuthType authType, string token = null)
        {
            string rawResponse = string.Empty;
            var modelToString = JsonConvert.SerializeObject(model);
            var payload = new StringContent(modelToString, Encoding.UTF8, "application/json");
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (HttpClient client = this.Client(apiUrl, authType, token, httpClientHandler))
                    {
                        HttpResponseMessage response = await client.PutAsync(apiUrl, payload);

                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            rawResponse = readTask.GetAwaiter().GetResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return rawResponse;
        }

        public async Task<string> Put(string apiUrl, string model, ApiServiceAuthType authType, string token = null)
        {
            string rawResponse = string.Empty;
            var payload = new StringContent(model, Encoding.UTF8, "application/json");
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (HttpClient client = this.Client(apiUrl, authType, token, httpClientHandler))
                    {
                        HttpResponseMessage response = await client.PutAsync(apiUrl, payload);

                        if (response.IsSuccessStatusCode)
                        {
                            var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            rawResponse = readTask.GetAwaiter().GetResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return rawResponse;
        }

        #region Private Method(s)
        private HttpClient Client(string apiUrl, ApiServiceAuthType authType, string token, HttpClientHandler httpClientHandler)
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(httpClientHandler);
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (authType == ApiServiceAuthType.Jwt)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
        #endregion
    }
}
