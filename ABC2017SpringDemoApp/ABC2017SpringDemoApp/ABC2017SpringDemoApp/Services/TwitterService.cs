using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABC2017SpringDemoApp.BusinessObjects;
using CoreTweet;

namespace ABC2017SpringDemoApp.Services
{
    public class TwitterService : ITwitterService
    {
        private Tokens Tokens { get; set; }

        public async Task InitializeAsync()
        {
            this.Tokens = Tokens.Create(
                Secrets.TwitterConsumerKey,
                Secrets.TwitterConsumerSecret,
                Secrets.TwitterAccessToken,
                Secrets.TwitterAccessTokenSecret);
            await this.Tokens.Account.VerifyCredentialsAsync();
        }

        public async Task<IEnumerable<TwitterSearchResult>> SearchImageAsync(string filter)
        {
            var results = await this.Tokens.Search.TweetsAsync(q => $"filter:images {filter} -RT", count => 500);
            return results.Where(x => x.Entities.Media != null)
                .Select(x => new TwitterSearchResult
                {
                    Id = x.Id,
                    User = x.User.ScreenName,
                    Text = x.Text,
                    Timestamp = x.CreatedAt,
                    Images = x.Entities.Media.Select(y => y.MediaUrl).ToArray(),
                });
        }
    }
}
