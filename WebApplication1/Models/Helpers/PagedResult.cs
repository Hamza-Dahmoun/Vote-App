using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Helpers
{
    public class PagedResult<T>
    {
        //this class is used in repositories when responding to jquery datatables
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }

        public PagedResult(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
