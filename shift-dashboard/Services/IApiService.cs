using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shift_dashboard.Services
{
    public interface IApiService
    {
        public Task<List<shift_dashboard.Model.Delegate>> DbDelegates();

        public Task<List<shift_dashboard.Model.Delegate>> ApiDelegates();

    }
}
