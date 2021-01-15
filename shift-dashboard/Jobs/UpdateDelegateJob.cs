using Microsoft.Extensions.Logging;
using Quartz;
using shift_dashboard.Data;
using shift_dashboard.Services;
using System.Threading.Tasks;

namespace shift_dashboard.Jobs
{
    [DisallowConcurrentExecution]
    public class UpdateDelegateJob : IJob
    {
        private readonly ILogger<UpdateDelegateJob> _logger;
        private ShiftDashboardContext _dbcontext;
        private ShiftApiService _shiftApiService;

        public UpdateDelegateJob(ILogger<UpdateDelegateJob> logger, ShiftDashboardContext dbcontext, ShiftApiService shiftApiService)
        {
            _logger = logger;
            _shiftApiService = shiftApiService;
            _dbcontext = dbcontext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            foreach (var sdelegate in _shiftApiService.FetchDelegates().Result )
            {
                _logger.LogInformation(sdelegate.username);
            }

            
            return Task.CompletedTask;
        }
    }
}