using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.Services
{
    public interface ITranslatorService
    {
        Task<string> TranslateToJapaneseAsync(string en);
    }
}
