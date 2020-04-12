using Covid19Data.Domain.Entities;
using Covid19Data.Domain.Repositories;
using Covid19Data.Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Data.Services.DataService
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _repository;
        private readonly ILogger<DataService> _logger;

        public DataService(ILogger<DataService> logger, IDataRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<DayData>> GetListData()
        {
            try
            {
                return await _repository.GetDataList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateInformation(List<DayData> data)
        {
            try
            {
                DateTime lastDate = await _repository.GetLastDate();

                List<DayData> dataToUpdate = data.Where(d => d.LastUpdatedAtSource > lastDate).ToList();

                if (dataToUpdate.Count > 0)
                    await _repository.UpdateData(dataToUpdate);

                else
                    throw new Exception("Não há dados a serem atualizados!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
