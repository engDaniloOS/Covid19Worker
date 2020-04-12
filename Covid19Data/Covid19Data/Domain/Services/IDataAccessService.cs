using System.Threading.Tasks;

namespace Covid19Data.Domain.Services
{
    public interface IDataAccessService
    {
        Task<string> GetDataList();
        Task<string> GetLastData();
    }
}