using System.Collections.Generic;

namespace OnShop.Domain.DTOs
{
    public class QueryList<T> where T : class
    {
        public IReadOnlyList<T> Data { get; set; }
        public int TotalCount { get; set; }
        public QueryList()
        {
            Data = new List<T>();
        }
    }
}