using Covid19Data.Domain.Entities;
using Covid19Data.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19Data.Infrastructure.Repositories
{
    public class DestinationEmailRepository : IDestinationEmailRepository
    {
        #region Parameters
        private readonly ApplicationContext _context;
        #endregion

        #region Constructors
        public DestinationEmailRepository(ApplicationContext context) => _context = context;
        #endregion

        #region Methods
        public async Task<List<DestinationEmail>> GetEmails() => await _context.DestinationEmails.ToListAsync();
        #endregion
    }
}
