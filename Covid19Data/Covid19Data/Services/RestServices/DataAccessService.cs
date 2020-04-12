using Covid19Data.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Covid19Data.Domain.Services
{
    public class DataAccessService : IDataAccessService
    {
        #region parameters
        private readonly IConfiguration _configuration;
        private readonly ILogger<DataAccessService> _logger;
        #endregion

        #region Constructors
        public DataAccessService(IConfiguration configuration, ILogger<DataAccessService> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public async Task<string> GetDataList() => await GetData(_configuration["urls:listUrl"]);

        public async Task<string> GetLastData() => await GetData(_configuration["urls:lastUrl"]);
        #endregion

        #region AuxMethods
        private async Task<string> GetData(string url)
        {
            try
            {
                HttpWebRequest webRequest = WebRequest.CreateHttp(url);
                webRequest.Method = "GET";

                using HttpWebResponse response = (HttpWebResponse)await webRequest.GetResponseAsync();
                using StreamReader reader = new StreamReader(response.GetResponseStream());

                if (response.StatusCode == HttpStatusCode.OK)
                    return reader.ReadToEnd().ToString();

                int statusCode = int.Parse(response.StatusCode.ToString());

                throw new HttpListenerException(statusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return string.Empty;
            }
        }
        #endregion
    }
}
