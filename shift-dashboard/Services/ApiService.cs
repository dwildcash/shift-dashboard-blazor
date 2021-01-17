using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using shift_dashboard.Data;
using shift_dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Delegate = shift_dashboard.Model.Delegate;

namespace shift_dashboard.Services
{
    public class ApiService : IApiService
    {
        private DashboardConfig _dashboardOptions;
        private readonly ILogger<ApiService> _logger;
        private DashboardContext _dbcontext;

        public ApiService(ILogger<ApiService> logger, DashboardConfig dashboardconfig, DashboardContext dbcontext)
        {
            _dashboardOptions = dashboardconfig;
            _logger = logger;
            _dbcontext = dbcontext;
        }

        public async Task<object> UpdateDelegateDb()
        {
            try
            {
                var DelegatesFromDb = _dbcontext.Delegates.ToListAsync().Result;
                var DelegatesFromApi = this.DelegatesFromApi().Result;

                foreach (var sdelegate in DelegatesFromApi)
                {
                    var ss = DelegatesFromDb.FirstOrDefault(x => x.Address == sdelegate.Address);

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

                await _dbcontext.SaveChangesAsync();

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return null;
            }
        }

        private async Task<List<Account>> ApiVoters(string publickey)
        {
            VoterApiResult voterApiResult;

            try
            {
                // Retreive Quote
                using (var hc = new HttpClient())
                {
                    var result = JObject.Parse(await hc.GetStringAsync(_dashboardOptions.APIUrl + "/api/delegates/voters?publicKey=" + publickey));
                    voterApiResult = JsonConvert.DeserializeObject<VoterApiResult>(result.ToString());

                    return voterApiResult.Success ? voterApiResult.Accounts : null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return null;
            }
        }

        /// <summary>
        /// Retreive All Delegate from Database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Delegate>> DelegatesFromDb()
        {
            return await _dbcontext.Delegates.AsNoTracking().ToListAsync();
        }

        /// <summary>
        ///  Retreive all Delegate From API
        /// </summary>
        /// <returns></returns>
        public async Task<List<Delegate>> DelegatesFromApi()
        {
            try
            {
                // Retreive Quote
                using (var hc = new HttpClient())
                {
                    var result = JObject.Parse(await hc.GetStringAsync(_dashboardOptions.APIUrl + "/api/delegates"));
                    var delegateResult = JsonConvert.DeserializeObject<DelegateApiResult>(result.ToString());

                    var result102to203 = JObject.Parse(await hc.GetStringAsync(_dashboardOptions.APIUrl + "/api/delegates?offset=102"));
                    var delegate102to203 = JsonConvert.DeserializeObject<DelegateApiResult>(result102to203.ToString());

                    var result204to305 = JObject.Parse(await hc.GetStringAsync(_dashboardOptions.APIUrl + "/api/delegates?offset=204"));
                    var delegate204to305 = JsonConvert.DeserializeObject<DelegateApiResult>(result204to305.ToString());

                    var result306to407 = JObject.Parse(await hc.GetStringAsync(_dashboardOptions.APIUrl + "/api/delegates?offset=306"));
                    var delegate306to407 = JsonConvert.DeserializeObject<DelegateApiResult>(result306to407.ToString());

                    // Comvine all the Delegates in Response

                    foreach (var o in delegate102to203.Delegates)
                    {
                        delegateResult.Delegates.Add(o);
                    }

                    foreach (var o in delegate204to305.Delegates)
                    {
                        delegateResult.Delegates.Add(o);
                    }

                    foreach (var o in delegate306to407.Delegates)
                    {
                        delegateResult.Delegates.Add(o);
                    }

                    return delegateResult.Success && delegate102to203.Success && delegate204to305.Success && delegate306to407.Success ? delegateResult.Delegates : null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return null;
            }
        }
    }
}