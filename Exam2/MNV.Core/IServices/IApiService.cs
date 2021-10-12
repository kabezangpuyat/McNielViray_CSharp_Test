using MNV.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNV.Core.IServices
{
    public interface IApiService
    {
        Task<string> Post<T>(string apiUrl, T model, ApiServiceAuthType authType, string token = null);
        Task<string> Post(string apiUrl, string model, ApiServiceAuthType authType, string token = null);
        Task<string> Get(string apiUrl, ApiServiceAuthType authType, string token = null);
        Task<string> Put<T>(string apiUrl, T model, ApiServiceAuthType authType, string token = null);
        Task<string> Put(string apiUrl, string model, ApiServiceAuthType authType, string token = null);
    }
}
