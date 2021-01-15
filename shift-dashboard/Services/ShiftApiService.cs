using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using shift_dashboard.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace shift_dashboard.Services
{
    public class ShiftApiService
    {
        private ShiftDashboardConfig _shiftOptions;

        public ShiftApiService(ShiftDashboardConfig shiftoptions)
        {
            _shiftOptions = shiftoptions;
        }


        /// <summary>
        /// The FetchVoters
        /// </summary>
        /// <param name="publickey">The publickey<see cref="string"/></param>
        /// <returns>The <see cref="Task{VotersResult}"/></returns>
        private async Task<List<VoterAccount>> FetchVoters(string publickey)
        {
            VoterApiResult voterApiResult;

            try
            {
                // Retreive Quote
                using (var hc = new HttpClient())
                {
                    var result = JObject.Parse(await hc.GetStringAsync(_shiftOptions.ShiftAPIUrl + "/api/delegates/voters?publicKey=" + publickey));
                    voterApiResult = JsonConvert.DeserializeObject<VoterApiResult>(result.ToString());

                    return voterApiResult.success ? voterApiResult.accounts : null;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.InnerException);
                return null;
            }
        }

        /// <summary>
        /// Fetch 407 top Delegates
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShiftDelegate>> FetchDelegates()
        {
            try
            {
                // Retreive Quote
                using (var hc = new HttpClient())
                {
                    var result = JObject.Parse(await hc.GetStringAsync(_shiftOptions.ShiftAPIUrl + "/api/delegates"));
                    var delegateResult = JsonConvert.DeserializeObject<DelegateApiResult>(result.ToString());

                    var result102to203 = JObject.Parse(await hc.GetStringAsync(_shiftOptions.ShiftAPIUrl + "/api/delegates?offset=102"));
                    var delegate102to203 = JsonConvert.DeserializeObject<DelegateApiResult>(result102to203.ToString());

                    var result204to305 = JObject.Parse(await hc.GetStringAsync(_shiftOptions.ShiftAPIUrl + "/api/delegates?offset=204"));
                    var delegate204to305 = JsonConvert.DeserializeObject<DelegateApiResult>(result204to305.ToString());

                    var result306to407 = JObject.Parse(await hc.GetStringAsync(_shiftOptions.ShiftAPIUrl + "/api/delegates?offset=306"));
                    var delegate306to407 = JsonConvert.DeserializeObject<DelegateApiResult>(result306to407.ToString());

                    // Merge Delegates 200 to 399
                    foreach (var o in delegate102to203.delegates)
                    {
                        delegateResult.delegates.Add(o);
                    }

                    foreach (var o in delegate204to305.delegates)
                    {
                        delegateResult.delegates.Add(o);
                    }

                    foreach (var o in delegate306to407.delegates)
                    {
                        delegateResult.delegates.Add(o);
                    }

                    return delegateResult.success && delegate102to203.success && delegate204to305.success && delegate306to407.success ? delegateResult.delegates : null;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.InnerException);
                return null;
            }
        }
    }
}