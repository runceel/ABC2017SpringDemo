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
        public string Caption { get; set; }
        public string JpCaption { get; set; }
        public string ThemeColor { get; set; }
        public TwitterSearchResult OriginalTweet { get; set; }
    }
}
