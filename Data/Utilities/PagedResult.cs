﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Utilities
{
    public class PagedResult<T> : PagedResultBase where T : class // phân trang cho object
    {
        public PagedResult()
        {
            Results = new List<T>();
        }
        public IList<T> Results { get; set; }
    }
}
