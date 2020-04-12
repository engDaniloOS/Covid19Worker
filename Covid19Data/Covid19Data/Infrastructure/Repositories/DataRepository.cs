using Covid19Data.Domain.Entities;
using Covid19Data.Domain.Repositories;
using Covid19Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Data.Repositories
{
    public class DataRepository : IDataRepository
    {
        #region Parameters
        private readonly ApplicationContext _context;
        #endregion

        #region Constructors
        public DataRepository(ApplicationContext context) => _context = context;
        #endregion

        #region Methods
        public async Task<List<DayData>> GetDataList() => await _context.DayDatas.ToListAsync();

        public Task<DayData> GetLastData()
        {
            throw new NotImplementedException();
        }

        public async Task<DateTime> GetLastDate()
        {
            DayData dayData = await _context.DayDatas
                                            .OrderByDescending(d => d.LastUpdatedAtSource)
                                            .FirstOrDefaultAsync();

            return dayData != null ? dayData.LastUpdatedAtSource : DateTime.MinValue;
        }

        public async Task UpdateData(List<DayData> dataToUpdate)
        {
            await _context.DayDatas.AddRangeAsync(dataToUpdate);
            await _context.SaveChangesAsync();
        } 
        #endregion
    }
}
