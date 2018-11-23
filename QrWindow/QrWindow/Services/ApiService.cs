using Newtonsoft.Json;
using QrWindow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QrWindow.Services
{
    class ApiService : IApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
        }

        public string BaseUri
        {
            get { return _client.BaseAddress.AbsoluteUri; }
            set
            {
                _client.BaseAddress = new Uri(value);
            }
        }


        public string Token => _client.DefaultRequestHeaders.GetValues("Authorization").First();


        public void SetValidationHeader(string MeStudentToken)
        {
            _client.DefaultRequestHeaders.Add("Authorization", MeStudentToken);
        }

        public void ClearValidationHeader()
        {
            _client.DefaultRequestHeaders.Remove("Authorization");
        }

        async Task<Response<T>> ExtractResonse<T>(HttpResponseMessage httpResponse)
        {
            var response = new Response<T>
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                StatusCode = (int)httpResponse.StatusCode,
                Message = httpResponse.ReasonPhrase
            };

            
            string result = await httpResponse.Content.ReadAsStringAsync();
            if (response.IsSuccess)
                response.Result = JsonConvert.DeserializeObject<T>(result);
            else
                response.ErrorResult = JsonConvert.DeserializeObject<ErrorResult>(result);

            return response;

        }

        private async Task<Response<T>> ExecuteRequest<T>(Func<Task<HttpResponseMessage>> apiRequest)
        {

            Response<T> response;

            
            try
            {
                var httpResponse = await apiRequest();
                response = await ExtractResonse<T>(httpResponse);

            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                response = Response<T>.GetErrorResponse(ex.Message);
            }

            

            return response;
        }

        #region GET


        public async Task<Response<T>> GetResopnseAsync<T>(string uriRequest, bool cachedResult = false, int days = 2)
        {
            return await ExecuteRequest<T>(async () => await _client.GetAsync(uriRequest));
        }

        #endregion

        #region POST

        public async Task<Response<T>> PostForResponseAsync<T>(string uriRequest)
        {
            return await ExecuteRequest<T>(async () => await _client.PostAsync(uriRequest, new StringContent("")));
        }


        public async Task<Response<T>> PostMultiPartFormAsync<T>(string uriRequest, MultipartFormDataContent content)
        {
            return await ExecuteRequest<T>(async () => await _client.PostAsync(uriRequest, content));
        }



        public async Task<Response<T>> PostForResponseAsync<T, Param>(string uriRequest, Param param)
        {
            string json = JsonConvert.SerializeObject(param);
            HttpContent content = new StringContent(json, Encoding.UTF8, "text/json");
            return await ExecuteRequest<T>(async () => await _client.PostAsync(uriRequest, content));
        }
        #endregion

        #region PUT

        public async Task<Response<T>> PutForGenericResponseAsync<T, Param>(string uriRequest, Param param)
        {

            string json = JsonConvert.SerializeObject(param);
            HttpContent content = new StringContent(json, Encoding.UTF8, "text/json");
            return await ExecuteRequest<T>(async () => await _client.PutAsync(uriRequest, content));
        }

        public async Task<Response<T>> PutForGenericResponseAsync<T>(string uriRequest)
        {
            HttpContent content = new StringContent("", Encoding.UTF8, "text/json");
            return await ExecuteRequest<T>(async () => await _client.PutAsync(uriRequest, content));

        }

        #endregion


        #region DELETE
        public async Task<Response<T>> DeleteForResponseAsync<T>(string uriRequest)
        {
            return await ExecuteRequest<T>(async () => await _client.DeleteAsync(uriRequest));
        }
        #endregion
    }
}
