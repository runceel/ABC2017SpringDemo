using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABC2017SpringDemoApp.BusinessObjects;
using Microsoft.ProjectOxford.Vision;

namespace ABC2017SpringDemoApp.Services
{
    public class VisionService : IVisionService
    {
        private IVisionServiceClient VisionServiceClient { get; }

        public VisionService()
        {
            this.VisionServiceClient = new VisionServiceClient(
                Secrets.CognitiveServiceComputerVisionAPIKey,
                Consts.CognitiveServicesEndpoint);
        }

        public async Task<IEnumerable<TaggedImage>> TaggingAsync(IEnumerable<TwitterSearchResult> tweets)
        {
            var images = tweets.SelectMany(x => x.Images.Select(y => (image: y, tweet: x)));
            var results = new List<TaggedImage>();
            foreach (var image in images)
            {
                var r = await this.VisionServiceClient.AnalyzeImageAsync(image.image, null);
                results.AddRange(r.Categories.Select(x => new TaggedImage
                {
                    Image = image.image,
                    Tag = x.Name,
                    OriginalTweet = image.tweet,
                }));
                // 10 call / 1sec
                await Task.Delay(100);
            }
            return results;
        }
    }
}
