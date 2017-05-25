using ABC2017SpringDemoApp.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.ViewModels
{
    public class TagGroupViewModel
    {
        public string Tag { get; set; }
        public IEnumerable<TaggedImage> Images { get; set; }
        public TwitterSearchResult ExampleTweet => this.Images.FirstOrDefault()?.OriginalTweet;
    }
}
