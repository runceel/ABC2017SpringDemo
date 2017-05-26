using Microsoft.Translator.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ABC2017SpringDemoApp.Services
{
    public class TranslatorService : ITranslatorService
    {
        private AzureAuthToken AzureAuthToken { get; }

        public TranslatorService()
        {
            this.AzureAuthToken = new AzureAuthToken(Secrets.CognitiveServiceTranslatorAPIKey);
        }

        public async Task<string> TranslateToJapaneseAsync(string en)
        {
            if (string.IsNullOrWhiteSpace(en))
            {
                return "";
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await this.AzureAuthToken.GetAccessTokenAsync()).Split(' ')[1]);
                var uri = $"http://api.microsofttranslator.com/v2/Http.svc/Translate?text={Uri.EscapeDataString(en)}&from=en&to=ja";
                var r = await httpClient.GetStringAsync(uri);
                return XDocument.Parse(r).Root.Value;
            }
        }
    }
}
