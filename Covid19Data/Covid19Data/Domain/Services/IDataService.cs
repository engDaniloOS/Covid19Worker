using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19Data.Domain.Entities;

namespace Covid19Data.Domain.Services
{
    public interface IDataService
    {
        Task UpdateInformation(List<DayData> data);
        Task<List<DayData>> GetListData();
    }
}