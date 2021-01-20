using Microsoft.Extensions.Logging;
using Quartz;
using shift_dashboard.Services;
using System;
using System.Threading.Tasks;

namespace shift_dashboard.Jobs
{
    [DisallowConcurrentExecution]
    public class UpdateDelegateStatJob : IJob
    {
        private readonly ILogger<UpdateDelegateStatJob> _logger;
        private IApiService _apiService;

        public UpdateDelegateStatJob(ILogger<UpdateDelegateStatJob> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogDebug("******************Started " + DateTime.Now);
            try
            {
                Task e = _apiService.UpdateDelegateStatDb();
                e.Wait();

                _logger.LogDebug("************************************ FInished " + DateTime.Now);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return null;
            }
        }
    }
}