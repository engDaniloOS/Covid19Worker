using Covid19Data.Domain.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Covid19Data.Services.SerializerServices
{
    public class JsonService : IJsonService
    {
        #region Parameters
        private readonly ILogger<JsonService> _logger;
        #endregion

        #region Constructors
        public JsonService(ILogger<JsonService> logger) => _logger = logger; 
        #endregion

        #region Methods
        public object Deserialize(string json, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(json, type, new JsonSerializerSettings
                {
                    Error = (sender, error) =>
                    {
                        error.ErrorContext.Handled = true;
                        _logger.LogError(error.ErrorContext.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
                    
        #endregion
    }
}
