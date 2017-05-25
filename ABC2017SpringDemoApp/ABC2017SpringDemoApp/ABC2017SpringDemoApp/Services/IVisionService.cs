using ABC2017SpringDemoApp.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.Services
{
    public interface IVisionService
    {
        Task<IEnumerable<TaggedImage>> TaggingAsync(IEnumerable<TwitterSearchResult> tweets);
    }
}
