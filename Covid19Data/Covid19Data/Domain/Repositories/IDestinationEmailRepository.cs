using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19Data.Domain.Entities;

namespace Covid19Data.Domain.Repositories
{
    public interface IDestinationEmailRepository
    {
        Task<List<DestinationEmail>> GetEmails();
    }
}