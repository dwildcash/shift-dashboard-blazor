using System.Collections.Generic;
using System.Threading.Tasks;

namespace shift_dashboard.Services
{
    public interface IApiService
    {
        public Task<List<shift_dashboard.Model.Delegate>> DelegatesFromDb();

        public Task<List<shift_dashboard.Model.Delegate>> DelegatesFromApi();

        public Task<object> UpdateDelegateDb();
    }
}