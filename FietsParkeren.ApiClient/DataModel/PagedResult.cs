using System;
using System.Collections.Generic;
using System.Text;

namespace FietsParkeren.ApiClient.DataModel
{
    public class PagedResult<T>
    {
        public T Data { get; set; }

        public int Total { get; set; }
    }
}
