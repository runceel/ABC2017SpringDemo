using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.BusinessObjects
{
    public class TaggedImage
    {
        public string Image { get; set; }
        public string Tag { get; set; }
        public TwitterSearchResult OriginalTweet { get; set; }
    }
}
