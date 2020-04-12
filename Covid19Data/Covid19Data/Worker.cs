using Covid19Data.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Covid19Data
{
    public class Worker : BackgroundService
    {
        #region Parameters
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBusinessService _businessService;
        #endregion

        #region Constructor
        public Worker(ILogger<Worker> logger, IBusinessService businessService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _businessService = businessService;
        }
        #endregion

        #region Methods
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker started to run at: {time}", DateTimeOffset.Now);

                int minutos = int.Parse(_configuration.GetSection("Tempo").Value);

                await _businessService.DataProcess();
             
                _logger.LogInformation("Worker finished to run at: {time}", DateTimeOffset.Now);
                
                await Task.Delay(1000 * 60 * minutos, stoppingToken);
            }
        } 
        #endregion
    }
}
