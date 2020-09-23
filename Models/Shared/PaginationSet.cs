using System.Collections.Generic;
using System.Linq;

namespace Models.Shared
{
    public class PaginationSet<T>
    {
        public int Page { set; get; }

        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }

        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
        public int MaxPage { set; get; } // max số trang hiển thị
        public IEnumerable<T> Items { set; get; }
        public string keyword { set; get; }
    }
}