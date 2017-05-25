﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.BusinessObjects
{
    public class TwitterSearchResult
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string[] Images { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string User { get; set; }
    }
}
