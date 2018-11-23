using QrWindow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QrWindow.Services
{
    public interface IApiService
    {
        string Token { get; }
        string BaseUri { get; set; }

        void SetValidationHeader(string MeStudentToken);
        void ClearValidationHeader();
        #region GET
        Task<Response<T>> GetResopnseAsync<T>(string uriRequest, bool cachedResult = false, int days = 2);
        #endregion

        #region PUT
        Task<Response<T>> PutForGenericResponseAsync<T, Param>(string uriRequest, Param param);
        Task<Response<T>> PutForGenericResponseAsync<T>(string uriRequest);
        #endregion

        #region POST
        Task<Response<T>> PostForResponseAsync<T, Param>(string uriRequest, Param param);
        Task<Response<T>> PostForResponseAsync<T>(string uriRequest);
        Task<Response<T>> PostMultiPartFormAsync<T>(string uriRequest, MultipartFormDataContent content);
        #endregion

        #region DELETE
        //Task<bool> Delete(string uriRequest);
        Task<Response<T>> DeleteForResponseAsync<T>(string uriRequest);
        #endregion
    }
}
