using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using shift_dashboard.Data;
using shift_dashboard.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace shift_dashboard.Jobs
{
    [DisallowConcurrentExecution]
    public class UpdateDelegateJob : IJob
    {
        private readonly ILogger<UpdateDelegateJob> _logger;
        private IApiService _apiService;
        private DashboardContext _dbcontext;

        public UpdateDelegateJob(ILogger<UpdateDelegateJob> logger, IApiService apiService, DashboardContext dbcontext)
        {
            _logger = logger;
            _apiService = apiService;
            _dbcontext = dbcontext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dbDelegates = _dbcontext.Delegates.ToListAsync().Result;
                var apiDelegates = _apiService.ApiDelegates().Result;

                foreach (var sdelegate in apiDelegates)
                {
                    var ss = dbDelegates.FirstOrDefault(x => x.Address == sdelegate.Address);

                    if (ss == null)
                    {
                        _dbcontext.Delegates.Add(sdelegate);
                    }
                    else
                    {
                        ss = sdelegate;
                        _dbcontext.Delegates.Update(ss);
                    }
                }

                _dbcontext.SaveChangesAsync();

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