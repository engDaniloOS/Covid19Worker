using Covid19Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19Data.Domain.Repositories
{
    public interface IDataRepository
    {
        Task<DayData> GetLastData();
        Task<DateTime> GetLastDate();
        Task UpdateData(List<DayData> dataToUpdate);
        Task<List<DayData>> GetDataList();
    }
}
