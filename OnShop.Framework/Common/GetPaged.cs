using System;
using System.Collections.Generic;
using System.Linq;
namespace OnShop.Framework.Common
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
    }

    public class PagedResults<T> : PagedResultBase where T : class
    {
        public IReadOnlyList<T> Data { get; set; }

        public PagedResults()
        {
            Data = new List<T>();
        }
        
    }
    
}