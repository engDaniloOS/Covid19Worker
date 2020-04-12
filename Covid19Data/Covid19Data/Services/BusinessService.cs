using Covid19Data.Domain.Entities;
using Covid19Data.Domain.Repositories;
using Covid19Data.Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19Data.Services
{
    public class BusinessService : IBusinessService
    {
        #region Parameters
        private readonly ILogger<BusinessService> _logger;

        private readonly IXmlService _xmlService;
        private readonly IJsonService _jsonService;
        private readonly IDataService _dataService;
        private readonly ISendEmailService _mailService;
        private readonly IDataAccessService _dataAccessService;
        #endregion

        #region Constructors
        public BusinessService(ILogger<BusinessService> logger,
                               IDataAccessService dataAccessService,
                               ISendEmailService mailService,
                               IJsonService jsonService,
                               IDataService dataService,
                               IXmlService xmlService)
        {
            _logger = logger;
            _xmlService = xmlService;
            _jsonService = jsonService;
            _dataService = dataService;
            _mailService = mailService;
            _dataAccessService = dataAccessService;
        }
        #endregion

        public async Task DataProcess()
        {
            try
            {
                List<DayData> listDayData = await GetData();
                
                var xlsStream = await _xmlService.GetXml(listDayData);

                await _mailService.SendEmail(xlsStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return;
            }
        }

        private async Task<List<DayData>> GetData()
        {
            string json = await _dataAccessService.GetDataList();

            List<DayData> listDayData;

            if (!string.IsNullOrWhiteSpace(json))
            {
                listDayData = (List<DayData>)_jsonService.Deserialize(json, typeof(List<DayData>));

                await _dataService.UpdateInformation(listDayData);
            }

            else
                listDayData = await _dataService.GetListData();

            return listDayData;
        }
    }
}
