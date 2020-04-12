using Covid19Data.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Covid19Data.Domain.Services
{
    public interface IXmlService
    {
        Task<byte[]> GetXml(List<DayData> data);
    }
}