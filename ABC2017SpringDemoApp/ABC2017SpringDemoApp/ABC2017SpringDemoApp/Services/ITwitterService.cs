using ABC2017SpringDemoApp.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.Services
{
    public interface ITwitterService
    {
        Task InitializeAsync();
        Task<IEnumerable<TwitterSearchResult>> SearchImageAsync(string filter);
    }
}
